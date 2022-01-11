using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ApplicationCore.Entities.ProjectAggregate;
using System;
using ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class ProjectController : Controller, IProjectService
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectQueryService ProjectQueryService;

        public ProjectController(ILogger<ProjectController> logger, IProjectQueryService _projectQueryService)
        {
            _logger = logger;
            this.ProjectQueryService = _projectQueryService;
        }
        [Route("Api/Projects/GetAll")]
        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await ProjectQueryService.GetProjects();
        }
        [Route("Api/Projects/Get/{id:int}")]
        [HttpGet]
        public async Task<Project> GetProject(int id)
        {
            return await ProjectQueryService.GetProject(id);
        }

        [Route("Api/Projects/Create/")]
        [HttpPost]
        public async Task<Project> CreateProject([Bind("ProjectName,CustomerName,CreatedDate,CreatedBy,StartDate,EndDate, IsComplete")] Project project)
        {
            try
            {
                if (project == null)
                    return null;
                return await ProjectQueryService.Create(project);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("Api/Projects/Edit/{id:int}")]
        [HttpPost]
        public async Task<ActionResult<string>> EditProject(int id, [Bind("ProjectName,CustomerName,CreatedDate,CreatedBy,StartDate,EndDate, IsComplete")] Project project)
        {
            return await ProjectQueryService.Edit(id, project);
        }
        [Route("Api/Projects/Delete/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                return await ProjectQueryService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
