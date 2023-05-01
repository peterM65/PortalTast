using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Core.Helper;
using Portal.Core.Interface;
using Portal.Core.Models;
using Portal.Infrastructure.Entities;

namespace Portal.Presentation.Controllers
{
    [Authorize(Roles = "Admin , HR ")]
    public class EmployeeController : Controller
    {
        #region Prop
        private readonly IEmployeeRep employee;
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;
        private readonly IDistrictRep district;
        private readonly ICityRep city;
        private readonly ICountryRep country;
        #endregion

        #region Ctor
        public EmployeeController(IEmployeeRep employee, IDepartmentRep department, IMapper mapper, IDistrictRep district, ICityRep city, ICountryRep country)
        {
            this.employee = employee;
            this.department = department;
            this.mapper = mapper;
            this.district = district;
            this.city = city;
            this.country = country;
        }
        #endregion

        #region Actions

        public async Task<IActionResult> Index(string MyName)
        {
            if (MyName != null)
            {
                var emps = await employee.GetAsync(emp => emp.IsDeleted == false && emp.IsActive == true && emp.Name.Contains(MyName));
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return View(data);
            }
            else
            {
                var emps = await employee.GetAsync(emp => emp.IsDeleted == false && emp.IsActive == true);
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return View(data);
            }



        }



        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var emps = await employee.GetByIdAsync(emp => emp.Id == id && emp.IsDeleted == false && emp.IsActive == true);
            var data = mapper.Map<EmployeeVM>(emps);
            return View(data);
        }

        // Draw Form

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.GetAsync(country => country.Id != null), "Id", "Name");
            return View();
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {

                ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
                ViewBag.CountryList = new SelectList(await country.GetAsync(country => country.Id != null), "Id", "Name");

                model.ImageName = FileUploader.UploadFile("Images", model.Image);
                model.CVName = FileUploader.UploadFile("CVs", model.CV);

                // Validation Check
                if (!ModelState.IsValid)
                    return View(model);
                var data = mapper.Map<Employee>(model);
                await employee.CreateAsync(data);
                //return View(model);

            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Employee");

        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            var emp = await employee.GetByIdAsync(emp => emp.Id == Id && emp.IsDeleted == false && emp.IsActive == true);

            var data = mapper.Map<EmployeeVM>(emp);
            return View(data);
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", model.DepartmentId);
                // Validation Check
                if (!ModelState.IsValid)
                    return View(model);
                var data = mapper.Map<Employee>(model);
                await employee.UpdateAsync(data);


            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Employee");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var emps = await employee.GetByIdAsync(emp => emp.Id == id && emp.IsDeleted == false && emp.IsActive == true);
            var data = mapper.Map<EmployeeVM>(emps);
            return View(data);
        }

        //save Data Into database
        [HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(EmployeeVM model)
        {
            try
            {
                FileUploader.RemoveFile("Images", model.ImageName);
                FileUploader.RemoveFile("Cvs", model.CVName);
                var data = mapper.Map<Employee>(model);
                await employee.DeleteAsync(data);

            }
            catch (Exception ex)
            {
                // Handle
                TempData["Error"] = "";

                // Log
            }
            return RedirectToAction("Index", "Employee");

        }

        #endregion

        #region Ajax Call
        [HttpPost]
        [HttpPost]
        public async Task<JsonResult> GetCityByCountryId(int CntryId)
        {
            var data = await city.GetAsync(city => city.CountryId == CntryId);
            return Json(data);
        }


        [HttpPost]
        public async Task<JsonResult> GetDistrictByCityId(int CtyId)
        {
            var data = await district.GetAsync(district => district.CityId == CtyId);
            return Json(data);
        }

        #endregion

    }
}
