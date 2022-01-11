using ApplicationCore.Entities.WorkAggregate;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class WorkQueryService: IWorkQueryService
    {
        private DbProjectWorkContext db;

        public WorkQueryService(DbProjectWorkContext _db)
        {
            db = _db;
        }

        public async Task<Work> Create(Work work)
        {
            var result = await db.AddAsync(work);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<string> Delete(int id)
        {
            var work = await GetWork(id);
            if (work == null)
            {
                return "Error when deleting work: Could not find work to remove";
            }
            try
            {
                db.Works.Remove(work);
                await db.SaveChangesAsync();
                return "Success"; //Todo: Is there a better way?
            }
            catch (DbUpdateException ex)
            {
                return string.Format("Error when deleting work: {0}", ex.ToString());
            }
        }

        public async Task<string> Edit(int id, Work work)
        {
            if (id != work.Id)
            {
                return "Error when updating work: Work is not found";
            }

            try
            {
                db.Works.Update(work);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!WorkExists(work.Id))
                {
                    return "Error when updating work: Work is not found";
                }
                else
                {
                    return string.Format("Error when updating work: {0}", ex.ToString());
                }
            }
            return "Success"; //Todo: Is there a better way?
        }
        private bool WorkExists(int id)
        {
            return db.Works.Any(e => e.Id == id);
        }
        public async Task<Work> GetWork(int id)
        {
            try
            {
                var work = await db.Works
                    .Include(o => o.Project)
                    .Include(o => o.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
                return work;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Work>> GetWorks()
        {
            try
            {
                return await db.Works.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> Stop(int userId)
        {
            try
            {
                var items = await db.Works.Where(w => w.UserId== userId && w.End == null).ToListAsync();
                items.ForEach(i => i.End = DateTime.Now);
                db.SaveChanges();
                return "Success";
            }
            catch
            {
                return "Error when stopping work for a user";
            }
        }

        public async Task<Work> GetActive(int userId)
        {
            try
            {
                var work = await db.Works
                    .Include(o => o.Project)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.End==null);
                return work;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Work>> GetWorkReport(int userId)
        {
            try
            {
                var lstWork = await db.Works
                    .Where(w => w.UserId == userId && w.End != null)
                    .Include(o => o.Project)
                    .Include(o => o.User)
                    .AsNoTracking()
                    .ToListAsync();


                return lstWork;
            }
            catch
            {
                throw;
            }
        }
        internal async Task<IEnumerable<Work>> GetWorkForUser(int userId)
        {
            try
            {
                var lstWork = await db.Works
                    .Where(w => w.UserId == userId && w.End != null)
                    .ToListAsync();
                return lstWork;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteAllWork(int userId)
        {
            var work = await GetWorkForUser(userId);
            if (work == null)
            {
                return "Error when deleting work: Could not find work to remove";
            }
            try
            {
                db.Works.RemoveRange(work);
                await db.SaveChangesAsync();
                return "Success"; //Todo: Is there a better way?
            }
            catch (DbUpdateException ex)
            {
                return string.Format("Error when deleting work: {0}", ex.ToString());
            }
        }
    }
}
