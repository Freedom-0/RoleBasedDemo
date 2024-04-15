using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Xml.Linq;


namespace RoleBasedDemo.Models
{
    public class UserViewModel
    {
        #region customer
        public string? AccountNo { get; set; }
        public string? DateOfBirth { get; set; }
        public string? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? Date {  get; set; } = DateTime.MinValue;
        public AccountType AccountType { get; set; }
        public string? Address {  get; set; }
        public string? Mobile {  get; set; }
        public string? Email { get; set; }
        public string? USER { get; set; }

        public string? Status1 { get; set; }
        public string? Approver {  get; set; }
        #endregion

        #region transction

        public int Amt {  get; set; }
        public string? Data1 { get; set; }
        public string? Data2 { get; set; }
        public string? Data3 { get; set; }
        public string? Data4 { get; set; }

        public string? Data5 { get; set; }
        public string? Data6 { get; set; }
        public string? Data7 { get; set; }
        public string? Data8 { get; set; }
        #endregion

        #region admin
        public int Code { get; set; }
        public string Name { get; set; }
        public string EmployeeName { get; set; }
        public string ROLE { get; set; }
        public string BRANCH { get; set; }
        public string MENU { get; set; }
        public string SUBMENU { get; set; }
        public int Status {  get; set; }
        public int MenuId { get; set; }
        public int BranchID { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public bool HasChild { get; set; }
        public string Url { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName  { get; set; }

      



        #endregion

        #region to convert list explictly
        public List<UserViewModel> PageMenuListzz { get; set; }
        public List<UserViewModel> PageSubMenuListzz { get; set; }

        #endregion
        // Initialize the list in the constructor
        #region LIST
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        
        //public List<SelectListItem> CustomerList { get; set; }

        public UserViewModel()
        {
            EmployeeList = new List<SelectListItem>();
            CustomerList= new List<SelectListItem>();
            RoleList = new List<SelectListItem>();
            BranchList = new List<SelectListItem>();
           
            //for list 
            PageMenuListzz = new List<UserViewModel>();
            PageSubMenuListzz = new List<UserViewModel>();

        }
        #endregion
    }

    public enum AccountType
    {   
        Saving,
        Current,
        Credit
    }
}