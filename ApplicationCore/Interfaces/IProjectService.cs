using ApplicationCore.Entities.ProjectAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IProjectService
    {
        public Task<Project> GetProject(int id);
        public Task<IEnumerable<Project>> GetProjects();
        public Task<Project> CreateProject([Bind("ProjectName,CustomerName,CreatedDate,CreatedBy,StartDate,EndDate, IsComplete")] Project project);
        public Task<ActionResult<string>> EditProject(int id, [Bind("ProjectName,CustomerName,CreatedDate,CreatedBy,StartDate,EndDate, IsComplete")] Project project);
        public Task<ActionResult<string>> Delete(int id);
    }
}
