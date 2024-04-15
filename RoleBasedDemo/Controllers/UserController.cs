using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedDemo.Extensions;
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using RoleBasedDemo.Services;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Reflection;

namespace RoleBasedDemo.Controllers


{
     [Authorize(Roles = "Maker, Checker, Admin, Super")]
    public class UserController : Controller
    {
        #region service called

        private readonly IHomeService _homeService;
        private readonly IRegisterService _registerService;
        private readonly IUserInfoService _userInfoService;

       

        public UserController(IHomeService homeservice, IRegisterService registerService, IUserInfoService userInfoService)
        {
            _homeService = homeservice;
            _registerService = registerService;
            _userInfoService = userInfoService;
        }
        #endregion

        public IActionResult CreateCustomer()
        {
            var data = new UserViewModel()
            {
                Name = User.Identity.GetUserName(),
                ROLE = User.Identity.GetUserROle(),
                BRANCH = User.Identity.GetUserLocation()
            };

            // Pass claims data to the view
            return View(data);
            //return View();
        }

       public IActionResult DoTransction()
       {

            var data = new UserViewModel() {
                CustomerList = Common.BindSelectionTools(_registerService.GetUserDetails(new UserViewModel(), "AddedCustomerList", "DEMO_SP")),
                Name = User.Identity.GetUserName(),
                ROLE = User.Identity.GetUserROle(),
                BRANCH = User.Identity.GetUserLocation(),
                Data1 = User.Identity.GetUserID()
            };
            return View(data);
       }



        [HttpPost]
        public IActionResult TransctionProceed(UserViewModel model)
        {

            if(model is null)
            {
                //TAmount, TUserName, TAccountNo, TUserPhone, 
                
                //is userid
                return Redirect("TransctionProceed");
            }
            else
            {
                
                model.Name = model.Name;
                model.ROLE = model.ROLE;
                model.BRANCH = model.BRANCH;
                model.Data1 = model.Data1;
                model.Amt = model.Amt;
                model.CustomerName = model.CustomerName;
                model.Mobile = model.Mobile;
                model.AccountNo = model.AccountNo;
                _ = _userInfoService.CustomerDetails(model, "TxnProcess", "CUSTOMER_INFO");
            }
            return Redirect("DoTransction");
        }
        //[HttpGet]
        //public IActionResult GetAccountDetails(string customerId)
        //{
        //    // Call your service method to get account details based on customer ID
        //    //var accountDetails = _userInfoService.GetAccountDetails(customerId);
        //    var model = new UserViewModel
        //    {
        //        Data1 = customerId,
        //    };

        //    var data= _userInfoService.DOTEST(model, "AccountDetails", "TEST");
        //    // Return the account details as JSON
        //    return Json(data);
        //}
        [HttpGet]
        public IActionResult GetAccountDetails(string customerId)
        {
            // Call your service method to get account details based on customer ID
            // var accountDetails = _userInfoService.GetAccountDetails(customerId);
            var model = new UserViewModel
            {
                Data1 = customerId,
            };

            // Assuming DOTEST returns a DataTable or a collection of simple objects
            var data = _userInfoService.DOTEST(model, "AccountDetails", "TEST");

            // Serialize the data to a JSON string
            string jsonData = JsonConvert.SerializeObject(data);

            // Return the JSON string as content
            return Content(jsonData, "application/json");
        }


        [HttpGet]
        public ActionResult DataTransactions(UserViewModel model)
        {
            // Fetch transaction data from your service or repository
            var transactions = _userInfoService.CustomerDetails(model, "PendingTxn", "CUSTOMER_INFO");
            string jsonData = JsonConvert.SerializeObject(transactions);
            // Return transaction data as JSON
            return Content(jsonData, "application/json");
        }


        [HttpPost]
        public ActionResult HandleTransaction(string action, string tid, string tusername, string taccountno, string approver)
        {
            if (action == "Accept")
            {
                // Logic to accept the transaction
                var model = new UserViewModel
                {
                    Data2 = tid , //as data 2 transction id
                    Status1 = "Approved",
                    CustomerName = tusername ,
                    AccountNo = taccountno ,
                    Approver = approver

                };

                // Assuming DOTEST returns a DataTable or a collection of simple objects
                _ = _userInfoService.CustomerDetails(model, "TxnProcess", "CUSTOMER_INFO");


            }
            else if (action == "Reject")
            {
                // Logic to accept the transaction
                var model = new UserViewModel
                {
                    Data2 = tid, //as data 2 transction id
                    Status1 = "Rejected",
                    CustomerName = tusername,
                    AccountNo = taccountno,
                    Approver = approver

                };

                // Assuming DOTEST returns a DataTable or a collection of simple objects
                _ = _userInfoService.CustomerDetails(model, "TxnProcess", "CUSTOMER_INFO");
            }


            return Redirect("DoTransction");
            // Return a JSON response indicating success or failure
        }


        [HttpPost]
        public IActionResult CreateCustomer(UserViewModel model)
        {
         
            if (model is null)
            {

                return Redirect("CreateCusotmer");
            }
            else
            {
                model.Name = model.Name;
                model.ROLE = model.ROLE;
                model.BRANCH = model.BRANCH;
                model.CustomerID = model.CustomerID;
                model.CustomerName = model.CustomerName;
                model.Address = model.Address;
                model.Mobile = model.Mobile;
                model.Email = model.Email;
                model.DateOfBirth = model.DateOfBirth;
                model.AccountType = model.AccountType;
                model.AccountNo = model.AccountNo;
                _ = _userInfoService.CustomerDetails(model, "NewCustomer", "CUSTOMER_INFO");
            }


            // Fetch role and branch data from RoleAssigned table
            return Redirect("CreateCustomer");
            
            
        }




    }
}
