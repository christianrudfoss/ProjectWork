using App.Models;
using ApplicationCore.Entities.ProjectAggregate;
using ApplicationCore.Entities.WorkAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using App.Extensions;

namespace App.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;

        private readonly IProjectService _projectService;
        private readonly IWorkService _workService;
        public ProjectController(ILogger<ProjectController> logger, IMapper mapper, IProjectService projectService, IWorkService workService)
        {
            _logger = logger;
            _mapper = mapper;
            _projectService = projectService;
            _workService = workService;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<ActionResult> Projects()
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                var lstProject = await _projectService.GetProjects();

                if (lstProject == null)
                    return NotFound();
                else
                {
                    IEnumerable<ProjectResponseModel> model = _mapper.Map<IEnumerable<ProjectResponseModel>>(lstProject);
                    
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectName,CustomerName,CreatedDate,CreatedBy,StartDate,EndDate, IsComplete")] CreateProjectRequestModel requestmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var model = _mapper.Map<Project>(requestmodel);
                model.CreatedBy = 2;
                model.CreatedDate = DateTime.Now;
                model.IsComplete = false;

                var result = await _projectService.CreateProject(model);
                if (result == null) return BadRequest("Could not create project");

                var responseModel = _mapper.Map<ProjectResponseModel>(result);

                return RedirectToAction(nameof(Details), new { id = responseModel.Id });
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var project = await GetProject(id);

                if (project == null)
                    return NotFound();
                else
                {
                    ProjectResponseModel model = _mapper.Map<ProjectResponseModel>(project);
                    foreach (var item in model.Works)
                        item.SetMinutesOfWork();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        private async Task<Project> GetProject(int? id)
        {
            try
            {
                if (id == null)
                    return null;

                var result = await _projectService.GetProject((int)id);

                if (result == null)
                    return null;
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                Project project = await GetProject(id);

                if (project == null)
                    return NotFound();
                else
                {
                    ProjectResponseModel model = _mapper.Map<ProjectResponseModel>(project);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProjectRequestModel requestmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (requestmodel == null )
                    return null;
                var model = _mapper.Map<Project>(requestmodel);

                var result = await _projectService.EditProject(id, model);

                if (result.Value.ToString().ToLower().Equals("success"))
                    return RedirectToAction(nameof(Details), new { id = model.Id });
                else
                {
                    ViewBag.Status = result;
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public async Task<IActionResult> Start(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                await StopWork(2);

                var model = new Work
                {
                    Start = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ProjectId = (int)id,
                    UserId = 2
                };

                var result = await _workService.CreateWork(model);
                if (result == null) return BadRequest("Could not create work");

                var responseModel = _mapper.Map<WorkResponseModel>(result);

                return RedirectToAction(nameof(WorkController.Edit),"Work", new { id = responseModel.Id });
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        private async Task<string> StopWork(int userId)
        {
            return await _workService.Stop(userId);
        }

        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                Project project = await GetProject(id);

                if (project == null)
                    return NotFound();
                else
                {
                    ProjectResponseModel model = _mapper.Map<ProjectResponseModel>(project);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                var result = await _projectService.Delete((int)id);

                if (result.Value.ToString().ToLower().Equals("success"))
                    return RedirectToAction(nameof(Projects));
                else
                {
                    ViewBag.Status = result;
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
    }
}
