using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Common.ResponseObjects;
using TodoAppNtierArchitecture.Dtos.WorkDtos;
using TodoAppNtierArchitecture.UI.Extensions;

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
            var response = await _workService.GetAll();
            
                return View(response.Data);

            

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
           
                var response=await _workService.Create(dto);
            return this.ResponsedRedirectToAction(response, "Index");
        
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);

        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
           
                var response=await _workService.Update(dto);
            return this.ResponsedRedirectToAction(response, "Index");
            
         
        }
        public async Task<IActionResult> Remove(int id)
        {
           var response= await _workService.Remove(id);
            return this.ResponsedRedirectToAction(response, "Index");
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
