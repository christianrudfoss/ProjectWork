using App.Models;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace App.ViewComponents
{
    [ViewComponent(Name = "ActiveWork")]
    public class ActiveWork : ViewComponent
    {
        private readonly IWorkService _workService;
        private readonly IMapper _mapper;
        public ActiveWork(IWorkService workService, IMapper mapper)
        {
            _workService = workService;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetActiveWorkAsync();
            return View(model);
        }
        private async Task<WorkResponseModel> GetActiveWorkAsync()
        {
            var _model = await _workService.GetActive(2);
            WorkResponseModel model = _mapper.Map<WorkResponseModel>(_model);
            return model;
        }
        [HttpPost, ActionName("StopWork")]
        [ValidateAntiForgeryToken]
        public async Task<IViewComponentResult> StopWork(int userId)
        {
            var result = await _workService.Stop(userId);
            return View();
        }
    }
}
