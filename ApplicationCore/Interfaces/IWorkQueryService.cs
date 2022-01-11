using ApplicationCore.Entities.WorkAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IWorkQueryService
    {
        public Task<IEnumerable<Work>> GetWorks();
        public Task<Work> GetWork(int id);
        public Task<Work> GetActive(int userId);
        public Task<string> Delete(int id);
        public Task<string> Edit(int id, Work project);
        public Task<Work> Create(Work project);

        public Task<string> Stop(int userId);
        public Task<IEnumerable<Work>> GetWorkReport(int userId);
        public Task<string> DeleteAllWork(int userId);
    }
}
