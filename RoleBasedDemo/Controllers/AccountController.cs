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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace RoleBasedDemo.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHomeService _homeService;
        private readonly IRegisterService _registerService;
        private readonly IUserInfoService _userInfoService;

        public AccountController(IHomeService homeservice, IRegisterService registerService, IUserInfoService userInfoService)
        {
            _homeService = homeservice;
            _registerService = registerService;
            _userInfoService = userInfoService;
        }
        public IActionResult Login()
        {
            return View();
        }

        #region validateuser

        [HttpPost]
        public JsonResult CheckCredentials(string userName, string password)
        {
            //var result = "1";
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
                            if (AddsessionData(userName, data.Rows[0]["FullName"].ToString(), data.Rows[0]["UserMail"].ToString(), data.Rows[0]["UID"].ToString()))
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


        private bool AddsessionData(string userName, string FullName, string UserMail, string UID )
        {
            try
            {
                var RoleExtracted = _homeService.User_Login(new LoginViewModel() { Activity = "ExtractRole", UserName = FullName.Trim() });
                var UserBranchExtracted = _homeService.User_Login(new LoginViewModel() { Activity = "ExtractLocation", UserName = FullName.Trim() });

                // Extract user roles from user details
                var roles = RoleExtracted.Rows[0]["UserRole"].ToString();
                var UserBranch = UserBranchExtracted.Rows[0]["BranchAssigned"].ToString();

                var claims = new List<Claim>
                {
                    new(Common.USERID, UID ?? ""),
                     new(Common.USERNAME, userName ?? ""),
                     new(Common.FULLNAME, FullName ?? ""),
                     new(Common.USERMAIL, UserMail ?? ""),
                     new(Common.USERROLE, roles ?? ""),
                     new(Common.USERLOCATION, UserBranch ?? "")
                 };

                // Adding roles to claims if available
                if (!string.IsNullOrEmpty(roles))
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles));
                    claims.Add(new Claim(ClaimTypes.Role, UserBranch));
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

        #region assignPermisson
        //[Authorize(Roles = "Admin")]
        //[Authorize(Policy = "RoleAndLocationPolicy")]
        public IActionResult AssignPermission()
        {
            var data = new UserViewModel()
            {


                EmployeeList = Common.BindSelectionTools(_registerService.GetUserDetails(new UserViewModel(), "EmployeeList", "DEMO_SP")),

                BranchList = Common.BindSelectionTools(_registerService.GetUserDetails(new UserViewModel(), "BranchList", "DEMO_SP")),

                RoleList = Common.BindSelectionTools(_registerService.GetUserDetails(new UserViewModel(), "RoleList", "DEMO_SP")),


                //--this is error PageMenuList = Common.ConvertDtToList<UserViewModel>(_registerService.GetUserDetails(new UserViewModel(), "GetMenuList", "DEMO_SP"))
                //to convert system collection list to microsoft.mvc.renderinglist
                PageMenuListzz = Common.ConvertDtToList<UserViewModel>(_registerService.GetUserDetails(new UserViewModel(), "GetMenuList", "DEMO_SP")),
                //UserViewModel.
                PageSubMenuListzz = Common.ConvertDtToList<UserViewModel>(_registerService.GetUserDetails(new UserViewModel(), "GetSubMenuList", "DEMO_SP"))


            };



            //foreach(var item in data.PageMenuList)
            //{
            //    item.PageSubMenuList =data.PageSubMenuList.ToList();
            //}
            return View(data);
        }


        #endregion


        #region userinfo

        [HttpPost]
        public IActionResult UpdateUserPermissions(int userId, int roleId, int branchId, List<string> menusubmenu)
        {
            try
            {
                // Create a UserViewModel object to hold the data

                foreach (var menuInfo in menusubmenu)
                {
                   
                    var menuIds = menuInfo.Split(',');

                    // Create a UserViewModel object to hold the data
                    var model = new UserViewModel
                    {
                        UserID = userId,
                        RoleID = roleId,
                        BranchID = branchId,
                        // Set the MenuID and SubMenuID based on the split values
                        MenuId= int.Parse(menuIds[0]),
                        SubMenuId = int.Parse(menuIds[1])
                    };

                   
                        var dt = _userInfoService.GetUserDetails(model, "UpdateOrInsert", "USERINFO");
                   
                    // Assuming menus is a list of strings representing menu IDs
                    // PageMenuListzz = menus
                };

               
                return Ok("Permissions updated successfully.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error occurred while updating permissions: " + ex.Message);
                return BadRequest("Error occurred while updating permissions.");
            }
        }
        #endregion

        #region binduserpermission

        [HttpGet]
        public IActionResult GetUserData(int userId)
        {
            var model = new UserViewModel
            {
                UserID = userId,
            };

            // Fetch role and branch data from RoleAssigned table
            var rolesandbranch = _userInfoService.GetUserDetails(model, "GetRolesAndBranch", "USERINFO");
            var menuAccess = _userInfoService.GetUserDetails(model, "GetMenuAccess", "USERINFO");

            if (rolesandbranch.Rows.Count > 0)
            {
                var roleId = Convert.ToInt32(rolesandbranch.Rows[0]["RoleID"]);
                model.RoleID = roleId;

                var branchId = Convert.ToInt32(rolesandbranch.Rows[0]["BranchId"]);
                model.BranchID = branchId;
            }

            // Create a UserViewModel to hold the fetched data
            var userData = new 
            {
                //UserID = model.UserID,
                model.RoleID,
                model.BranchID,
                MenuAccess = new List<string>() // Initialize list to hold menu access data
            };

            // Process menu access data and add to userData object
            foreach (DataRow row in menuAccess.Rows)
            {
                int menuId = Convert.ToInt32(row["MenuId"]);
                int submenuId = Convert.ToInt32(row["MenuSubId"]);
                string menuAccessString = $"{menuId},{submenuId}";
                userData.MenuAccess.Add(menuAccessString);
            }

            return Json(userData);
        }

        #endregion
    }
}
