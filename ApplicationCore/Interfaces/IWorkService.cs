using ApplicationCore.Entities.WorkAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IWorkService
    {
        public Task<Work> GetWork(int id);
        public Task<Work> GetActive(int userId);
        public Task<IEnumerable<Work>> GetAllWork();
        public Task<Work> CreateWork([Bind("ProjectId,UserId,CreatedDate,Start,End,Description")] Work project);
        public Task<ActionResult<string>> EditWork(int id, [Bind("ProjectId,UserId,CreatedDate,Start,End,Description")] Work work);
        public Task<ActionResult<string>> Delete(int id);
        public Task<string> Stop(int userId);
        public Task<IEnumerable<Work>> GetWorkReport(int userId);
        public Task<ActionResult<string>> DeleteAllWork(int userId);
    }
}
