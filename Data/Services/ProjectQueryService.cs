using ApplicationCore.Entities.ProjectAggregate;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class ProjectQueryService : IProjectQueryService
    {
        private DbProjectWorkContext db;

        public ProjectQueryService(DbProjectWorkContext _db)
        {
            db = _db;
        }

        public async Task<Project> Create(Project project)
        {
            var result = await db.AddAsync(project);
            await db.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<string> Delete(int id)
        {
            var project = await GetProject(id);
            if (project == null)
            {
                return "Error when deleting project: Could not find project to remove";
            }
            try
            {
                db.Projects.Remove(project);
                await db.SaveChangesAsync();
                return "Success"; //Todo: Is there a better way?
            }
            catch (DbUpdateException ex)
            {
                return string.Format("Error when deleting project: {0}", ex.ToString());
            }
        }

        public async Task<string> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return "Error when updating project: Project is not found";
            }

            try
            {
                db.Projects.Update(project);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProjectExists(project.Id))
                {
                    return "Error when updating project: Project is not found";
                }
                else
                {
                    return string.Format("Error when updating project: {0}", ex.ToString());
                }
            }
            return "Success"; //Todo: Is there a better way?

        }
        private bool ProjectExists(int id)
        {
            return db.Projects.Any(e => e.Id == id);
        }
        public async Task<Project> GetProject(int id)
        {
            try
            {
                var project = await db.Projects
                    .Include(w => w.Works)
                    .ThenInclude(u => u.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
                return project;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            try
            {
                return await db.Projects.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
