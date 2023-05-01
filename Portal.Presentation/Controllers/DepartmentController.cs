using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Portal.Core.Interface;
using Portal.Core.Models;
using Portal.Core.Repository;
using Portal.Infrastructure.Database;
using Portal.Infrastructure.Entities;

namespace Portal.Presentation.Controllers
{
    [Authorize(Roles = "Admin , HR ")]
    public class DepartmentController : Controller
    {

        //DepartmentRep Department = new DepartmentRep();
        //Tightly Coupling
        //DepartmentRep department;

        //Loosely Coupling

        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRep department ,IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }


        #region Get
        public async Task<IActionResult> Index()
        {
            var depts = await department.GetAsync();

            var data = mapper.Map<IEnumerable<DepartmentVM>>(depts);

            return View(data);
        }
        #endregion

        // Draw Form
        #region Create
        public IActionResult Create()
        {
            return View();
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM model)
        {
            try
            {

                // Validation Check
                if (!ModelState.IsValid)
                    return View(model);
                var data = mapper.Map<Department>(model);
                await department.CreateAsync(data);
                //return View(model);

            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Department");

        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var depts = await department.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(depts);

            return View(data);
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM model)
        {
            try
            {

                // Validation Check
                if (!ModelState.IsValid)
                    return View(model);
                var data = mapper.Map<Department>(model);
                await department.UpdateAsync(data);
                //return View(model);

            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Department");

        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var depts = await department.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(depts);
            return View(data);
        }

        //save Data Into database
        [HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(DepartmentVM model)
        {
            try
            {
                var data = mapper.Map<Department>(model);
                await department.DeleteAsync(data);
                
            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Department");

        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var depts = await department.GetByIdAsync(id);
            var data = mapper.Map<DepartmentVM>(depts);

            return View(data);
        }

        #endregion
    }
}
