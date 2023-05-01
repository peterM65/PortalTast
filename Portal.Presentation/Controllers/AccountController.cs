using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Core.Models;
using Portal.Infrastructure.Extend;
using System.Data;
using System.Diagnostics;

namespace Portal.Presentation.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userMAnager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userMAnager, SignInManager<ApplicationUser> signInManager ,IMapper mapper)
        {
            this.userMAnager = userMAnager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        #region Registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            
            var user = mapper.Map<ApplicationUser>(model);
            var result =await userMAnager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }



            
            return View(user);
        }

        #endregion

        #region Login
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await userMAnager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["error"] = "user not found";
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(user: user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "account not found");
                }
            }
            return View(model);
        }

        #endregion

        #region Sign Out
        public async Task<IActionResult> LogOFF()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {

            var user = await userMAnager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                // Generate Token
                var token = await userMAnager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);


                EventLog log = new EventLog();
                log.Source = "Hr System";
                log.WriteEntry(passwordResetLink, EventLogEntryType.Information);

            }

            return RedirectToAction("ConfirmtionForgetPassword");
        }
        

        public IActionResult ConfirmtionForgetPassword()
        {
            return View();
        }



        #endregion

        #region  Reset Password
        public IActionResult ResetPassword(string Email, string Token)
        {

            if (Email == null || Token == null)
                TempData["error"] = "account not registered";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {

            var user = await userMAnager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userMAnager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmationResetPassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);

        }


        public IActionResult ConfirmationResetPassword()
        {
            return View();
        }

        #endregion
    }
}
