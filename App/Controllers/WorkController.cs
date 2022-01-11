using App.Models;
using ApplicationCore.Entities.WorkAggregate;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using App.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace App.Controllers
{
    public class WorkController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;

        private readonly IWorkService _workService;
        public WorkController(ILogger<ProjectController> logger, IMapper mapper, IWorkService workService)
        {
            _logger = logger;
            _mapper = mapper;
            _workService = workService;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                Work work = await GetWork(id);

                if (work == null)
                    return NotFound();
                else
                {
                    WorkResponseModel model = _mapper.Map<WorkResponseModel>(work);
                    model.SetMinutesOfWork();
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
        public async Task<IActionResult> Edit(int id, UpdateWorkRequestModel requestmodel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var model = _mapper.Map<Work>(requestmodel);

                var result = await _workService.EditWork(id, model);

                if (result.Value.ToString().ToLower().Equals("success"))
                    return RedirectToAction("Details", "Project", new { id = model.ProjectId });
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
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var work = await GetWork(id);

                if (work == null)
                    return NotFound();
                else
                {
                    WorkResponseModel model = _mapper.Map<WorkResponseModel>(work);
                    model.SetMinutesOfWork();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        private async Task<Work> GetWork(int? id)
        {
            try
            {
                if (id == null)
                    return null;
                
                var result = await _workService.GetWork((int)id);
                if (result == null)
                    return null;
                else
                    return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                Work work = await GetWork(id);
                

                if (work == null)
                    return NotFound();
                else
                {
                    WorkResponseModel model = _mapper.Map<WorkResponseModel>(work);
                    model.SetMinutesOfWork();
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
        public async Task<IActionResult> DeleteConfirmed(int? id, int? ProjectId)
        {
            if (id == null)
                return NotFound();
            try
            {
                var result = await _workService.Delete((int)id);

                if (result.Value.ToString().ToLower().Equals("success"))
                {
                    if (ProjectId == null)
                        return RedirectToAction("Projects", "Project");
                    else
                        return RedirectToAction("Details", "Project", new { id = ProjectId });
                }
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
        [HttpPost, ActionName("StopWork")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StopWork(int? userId, string urlFull)
        {
            try
            {
                if (userId == null)
                    return NotFound();

                var result = await _workService.Stop((int)userId);

                return Redirect(urlFull);
                
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        public async Task<ActionResult> WorkReport()
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                if (TempData["StatusSuccess"] != null) ViewBag.StatusSuccess = TempData["StatusSuccess"].ToString();
                if (TempData["StatusError"] != null) ViewBag.StatusError = TempData["StatusError"].ToString();

                var lstWork = await _workService.GetWorkReport(2);

                if (lstWork == null)
                    return NotFound();
                else
                {
                    IEnumerable<WorkResponseModel> _model = _mapper.Map<IEnumerable<WorkResponseModel>>(lstWork);
                    foreach (var modelItem in _model)
                        modelItem.SetMinutesOfWork();

                     var model = _model
                    .GroupBy(x => new { x.UserId, x.UserName, x.ProjectId, x.ProjectName, x.Description })
                    .Select(g => new WorkResponseModel
                    {
                        UserId = g.Key.UserId,
                        UserName = g.Key.UserName,
                        ProjectId = g.Key.ProjectId,
                        ProjectName = g.Key.ProjectName,
                        Description = g.Key.Description,
                        MinutesOfWork = g.Sum(y => y.MinutesOfWork)
                    })
                    .OrderBy(x => x.ProjectName)
                    .ThenBy(x => x.Description);

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
        [HttpPost, ActionName("DeleteAllMyWork")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllMyWork()
        {
            try
            {
                var result = await _workService.DeleteAllWork(2);

                if (result.Value.ToString().ToLower().Equals("success"))
                {
                    TempData["StatusSuccess"] = "Your report is now cleared";
                    return RedirectToAction("WorkReport", "Work");
                }
                else
                {
                    TempData["StatusError"] = string.Format("An error has occured: {0}",result);
                    return RedirectToAction("WorkReport", "Work");
                }

            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
        }
    }
}
