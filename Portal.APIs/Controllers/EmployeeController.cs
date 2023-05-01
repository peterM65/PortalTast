using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Core.Helper;
using Portal.Core.Interface;
using Portal.Core.Models;
using Portal.Infrastructure.Entities;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Portal.APIs.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        #region Prop
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        #endregion

        #region Ctor
        public EmployeeController(IEmployeeRep employee, IMapper mapper,IConfiguration configuration)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        #endregion

        #region Actions
        [HttpGet,Route("~/api/GetEmployees")]
        public async Task<IActionResult> GetEmployee()
        {
            try 
            {
                var emps = await employee.GetAsync(emp => emp.IsDeleted == false && emp.IsActive == true);
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return Ok(new ApiResponse< IEnumerable < EmployeeVM >>() 
                {
                     Code = configuration["Response:Success:Code"]
                    ,Status= configuration["Response:Success:Code"],
                     Success=data 
                });
            }
            catch(Exception  ex) 
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Code"],
                    Error = ex.Message
                });
            } 
            
             
        }
        [HttpGet,Route("~/api/GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try 
            {
                var emps = await employee.GetByIdAsync(emp => emp.Id==id && emp.IsDeleted == false && emp.IsActive == true);
                var data = mapper.Map<EmployeeVM>(emps);
                return Ok(new ApiResponse < EmployeeVM >() 
                {
                     Code = configuration["Response:Success:Code"]
                    ,Status= configuration["Response:Success:Code"],
                     Success=data 
                });
            }
            catch(Exception  ex) 
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Code"],
                    Error = ex.Message
                });
            } 
            
             
        }
        //save Data Into database
        [HttpPost, Route("~/api/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM model)
        {
            try
            {

                // Validation Check
                if (!ModelState.IsValid)
                    return NotFound("Validation Error");
                var data = mapper.Map<Employee>(model);
                await employee.CreateAsync(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Created:Code"]
                    ,
                    Status = configuration["Response:Created:Code"]
                });

            }
            catch (Exception ex)
            {
                // Handle
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Code"],
                    Error = ex.Message
                });


            }
        }
        [HttpPut, Route("~/api/PutEmployee")]
        public async Task<IActionResult> PutEmployee(EmployeeVM model)
        { 
            try
            {

                if (!ModelState.IsValid)
                    return NotFound("Validation Error");

                var data = mapper.Map<Employee>(model);
                await employee.UpdateAsync(data);


                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Updated:Code"],
                    Status = configuration["Response:Updated:Status"]
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }

        [HttpDelete, Route("~/api/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeVM model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return NotFound("Validation Error");

                var data = mapper.Map<Employee>(model);
                await employee.DeleteAsync(data);


                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Updated:Code"],
                    Status = configuration["Response:Updated:Status"]
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }
        #endregion


    }
}
