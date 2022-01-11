using ApplicationCore.Entities.WorkAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    public class WorkController : Controller, IWorkService
    {
        private readonly IWorkQueryService WorkQueryService;

        public WorkController(IWorkQueryService _workQueryService)
        {
            WorkQueryService = _workQueryService;
        }
        [Route("Api/Work/Create/")]
        [HttpPost]
        public async Task<Work> CreateWork([Bind(new[] { "ProjectId,UserId,CreatedDate,Start,End,Description" })] Work work)
        {
            try
            {
                if (work == null)
                    return null;
                return await WorkQueryService.Create(work);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Route("Api/Work/Get/{id:int}")]
        [HttpGet]
        public async Task<Work> GetWork(int id)
        {
            return await WorkQueryService.GetWork(id);
        }
        [Route("Api/Work/GetAll")]
        [HttpGet]
        public async Task<IEnumerable<Work>> GetAllWork()
        {
            return await WorkQueryService.GetWorks();
        }
        [Route("Api/Work/Edit/{id:int}")]
        [HttpPost]
        public async Task<ActionResult<string>> EditWork(int id, [Bind("ProjectId,UserId,CreatedDate,Start,End,Description")] Work work)
        {
            return await WorkQueryService.Edit(id, work);
        }
        [Route("Api/Work/Delete/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                return await WorkQueryService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Api/Work/Stop/{userId:int}")]
        [HttpGet]
        public async Task<string> Stop(int userId)
        {
            try
            {
                return await WorkQueryService.Stop(userId);
            }
            catch (Exception)
            {
                return "Error when stopping work for a user";
            }
        }

        [Route("Api/Work/GetActive/{userId:int}")]
        [HttpGet]
        public async Task<Work> GetActive(int userId)
        {
            return await WorkQueryService.GetActive(userId);
        }

        [Route("Api/Work/GetWorkReport/{userId:int}")]
        [HttpGet]
        public async Task<IEnumerable<Work>> GetWorkReport(int userId)
        {
            return await WorkQueryService.GetWorkReport(userId);
        }
        [Route("Api/Work/DeleteAllWork/{userId:int}")]
        [HttpGet]
        public async Task<ActionResult<string>> DeleteAllWork(int userId)
        {
            try
            {
                return await WorkQueryService.DeleteAllWork(userId);
            }
            catch (Exception ex)
            {
                return string.Format("Error when deleting work: {0}", ex.ToString());
            }
        }
    }
}
