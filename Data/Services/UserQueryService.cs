using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class UserQueryService : IUserQueryService
    {
        private DbProjectWorkContext db;

        public UserQueryService(DbProjectWorkContext _db)
        {
            db = _db;
        }

        public async Task<User> Create(User user)
        {
            var result = await db.AddAsync(user);
            await db.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<string> Delete(int id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return "Error when deleting user: Could not find user to remove";
            }
            try
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return "Success"; //Todo: Is there a better way?
            }
            catch (DbUpdateException ex)
            {
                return string.Format("Error when deleting user: {0}",ex.ToString());
            }
        }

        public async Task<string> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return "Error when updating user: User is not found";
            }

            try
            {
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(user.Id))
                {
                    return "Error when updating user: User is not found";
                }
                else
                {
                    return string.Format("Error when updating user: {0}", ex.ToString());
                }
            }
            return "Success"; //Todo: Is there a better way?

        }
        private bool UserExists(int id)
        {
            return db.Users.Any(e => e.Id == id);
        }
        public async Task<User> GetUser(int id)
        {
            try
            {
                var user = await db.Users
                    .Include(u => u.Work)
                    .ThenInclude(w => w.Project)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            catch
            {
                throw;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return db.Users.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
