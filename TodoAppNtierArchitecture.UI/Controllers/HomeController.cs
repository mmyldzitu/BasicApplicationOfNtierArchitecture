using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Dtos.WorkDtos;

namespace TodoAppNtierArchitecture.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var workList = await _workService.GetAll();
            return View(workList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _workService.Create(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dto = await _workService.GetById(id);
            return View(new WorkUpdateDto()
            {
                Id = dto.Id,
                Definition = dto.Definition,
                isCompleted = dto.isCompleted
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _workService.Update(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _workService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
