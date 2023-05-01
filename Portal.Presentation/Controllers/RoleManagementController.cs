using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Core.Models;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;
using static System.Net.Mime.MediaTypeNames;

namespace Portal.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleManagementController : Controller
    {

        #region prop
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        #endregion

        #region ctor
        public RoleManagementController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        #endregion
        
        #region Retrieve Roles
        public IActionResult Index()
        {
            var users = roleManager.Roles.ToList();
            return View(users);

        }
        #endregion

        #region Create Roles
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //save Data Into database
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole model)
        {


            var result = await roleManager.CreateAsync(model);
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

        #region Update Roles
        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ApplicationRole model)
        {
            var result = await roleManager.UpdateAsync(model);

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

        #region Delete Roles
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationRole model)
        {
            var result = await roleManager.DeleteAsync(model);

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

        #region Add Or Remove User With Roles
        
        public async Task<IActionResult> AddOrRemoveUser(string RoleId)
        {
            ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVM>();

            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVM()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < model.Count)
                    continue;
            }

            return RedirectToAction("Update", new { id = RoleId });
        }
        #endregion
    }
}
