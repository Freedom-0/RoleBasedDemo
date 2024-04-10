using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using RoleBasedDemo.Services;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace RoleBasedDemo.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHomeService _homeService;
        private readonly IRegisterService _registerService;

        public AccountController(IHomeService homeservice, IRegisterService registerService)
        {
            _homeService = homeservice;
            _registerService = registerService;
        }
        public IActionResult Login()
        {
            return View();
        }

        #region validateuser

        [HttpPost]
        public JsonResult CheckCredentials(string userName, string password)
        {
            var result = "1";
            var msg = Common.ERROR;

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return Json(new { result = "0", msg = "userId and Password are required" });
                }
                else
                {
                    PrincipalContext pc = new PrincipalContext(ContextType.Machine);

                    var data = _homeService.User_Login(new LoginViewModel() { Activity = "teST", UserName = userName.Trim() });
                    if (data != null && data.Rows.Count > 0)
                    {
                        //string userername = data.Rows[0]["Username"].ToString().TrimEnd();
                        if (CheckPassword(pc, userName, password, _homeService))
                        {
                            if (AddsessionData(userName, data.Rows[0]["FullName"].ToString(), data.Rows[0]["UserMail"].ToString()))
                            {
                                TempData["SuccessMessage"] = "Login successful!";

                                // Redirect to the Dashboard page
                                return Json(new { result = "1", msg = "Login successful!", redirectUrl = Url.Action("Index", "Home") });
                            }
                            else
                            {
                                return Json(new { result = "0", msg });
                            }
                        }
                    }
                }

                return Json(new { result = "0", msg });
            }
            catch (Exception)
            {
                return Json(new { result = "0", msg });
            }
        }


        private static bool CheckPassword(PrincipalContext pc, string userName, string password, IHomeService homeService)
        {
            if (pc.ValidateCredentials(userName, password))
            {
                return true;
            }

            DataTable result = homeService.User_Login(new LoginViewModel() { Activity = "validate_user", PassWord = password, UserName = userName.Trim() });
            // return result.Rows.Count > 0;
            if (result.Rows.Count == 1 && Convert.ToInt32(result.Rows[0]["result"]) == 1)
            {
                return true;
            }

            // Otherwise, return false (credentials don't match)
            return false;
        }


        private bool AddsessionData(string userName, string FullName, string UserMail)
        {
            try
            {
                var RoleExtracted = _homeService.User_Login(new LoginViewModel() { Activity = "ExtractRole", UserName = FullName.Trim() });

                // Extract user roles from user details
                var roles = RoleExtracted.Rows[0]["UserRole"].ToString();

                var claims = new List<Claim>
        {
                     new Claim(Common.USERNAME, userName ?? ""),
                     new Claim(Common.FULLNAME, FullName ?? ""),
                     new Claim(Common.USERMAIL, UserMail ?? "")
                 };

                // Adding roles to claims if available
                if (!string.IsNullOrEmpty(roles))
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles));
                }

                ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authProperties = new()
                {
                    IsPersistent = true,
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region LOGOUT   
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region regisrtration

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = _registerService.RegisterUser(model);

                if (success)
                {
                    //TempData["SuccessMessage"] = "Registration successful!";
                    TempData["RegistrationStatus"] = 1; // Registration successful
                    return RedirectToAction("Register");
                }
                else
                {
                    // Handle registration failure
                    //ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    TempData["RegistrationStatus"] = 0; // Registration failed
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    return View("Register", model);
                }
            }

            // If model state is not valid, return to the registration page with validation errors
            return View("Register", model);
        }





        #endregion
    }
}
