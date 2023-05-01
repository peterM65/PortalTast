using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Portal.Core.Models;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;
using System.Data;

namespace Portal.Presentation.Controllers
{
    [Authorize (Roles ="Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        #region Retrieve Users

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);

        }


        #endregion

        #region Update Users
        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            return View(user);
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser model)
        {
            

                var result = await userManager.UpdateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                { 
                    foreach(var item in result.Errors) 
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            return View (model);

        }


        #endregion

        #region Delete Users

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            return View(user);
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser model)
        {


            var result = await userManager.DeleteAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);

        }

        #endregion

    }
}
