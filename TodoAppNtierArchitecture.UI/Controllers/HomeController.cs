using AutoMapper;
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
           
                await _workService.Create(dto);
                return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dto = await _workService.GetById<WorkUpdateDto>(id);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
           
                await _workService.Update(dto);
                return RedirectToAction("Index");
            
            
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _workService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
