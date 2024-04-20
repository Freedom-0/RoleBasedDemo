
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Drawing;
using System.Transactions;
using System.Security.Cryptography;

namespace RoleBasedDemo.Services { 

    public interface IUserInfoService
    {
          
           DataTable GetUserDetails(UserViewModel model,string activity, string storedProcedureName);
          DataTable CustomerDetails(UserViewModel model, string activity, string storedProcedureName);
        DataTable  DOTEST(UserViewModel model, string activity, string storedProcedureName);

        Task<bool> CheckUserAccessAsync(IEnumerable<string> roles, IEnumerable<string> locations, string requestUrl);

        bool CheckUsernameExistence(string username);
        bool CheckEmailExistence(string email);

    }

    public class UserInfoService : IUserInfoService
    {
        private readonly ConnectionStrings _connectionStrings;
        
        public UserInfoService(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }

        #region reusable datatable
        public DataTable GetUserDetails(UserViewModel model, string activity, string storedProcedureName)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                con.Open();
                using SqlCommand cmd = new(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Activity ", activity);
                cmd.Parameters.AddWithValue("@UserID"   ,model.UserID);
                cmd.Parameters.AddWithValue("@RoleID"   , model.RoleID);
                cmd.Parameters.AddWithValue("@BranchID ", model.BranchID);
                cmd.Parameters.AddWithValue("@MenuID", model.MenuId);
                cmd.Parameters.AddWithValue("@SubMenuID", model.SubMenuId);
               
                using SqlDataAdapter da = new(cmd);
                DataTable dt = new();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {                   
                    Console.WriteLine("Error during data retrieval: " + ex.Message);     
                    throw;
                }

                return dt;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
        #endregion

        #region just a test
        public async Task<bool> CheckUserAccessAsync(IEnumerable<string> roles, IEnumerable<string> locations, string requestUrl)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                await con.OpenAsync();
                using SqlCommand cmd = new("YourStoredProcedureName", con); // Replace "YourStoredProcedureName" with the actual name of your stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Roles", string.Join(",", roles));
                cmd.Parameters.AddWithValue("@Locations", string.Join(",", locations));
                cmd.Parameters.AddWithValue("@RequestUrl", requestUrl);

                var result = await cmd.ExecuteScalarAsync();

                // Example: Check if the result indicates that the user has access
                // Replace this logic with your actual implementation based on the result returned from the stored procedure
                if (result != null && result is int accessResult && accessResult == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                Debug.WriteLine("Error: " + ex.Message);
                return false; // Return false in case of an error
            }
        }
        #endregion

        #region customer
        public DataTable CustomerDetails(UserViewModel model, string activity, string storedProcedureName)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                con.Open();
                using SqlCommand cmd = new(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //parameters for customer ..
                cmd.Parameters.AddWithValue("@Activity", activity);
                
                cmd.Parameters.AddWithValue("@CustomerName", model.CustomerName);

                cmd.Parameters.AddWithValue("@CID", model.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerAddress", model.Address);
                cmd.Parameters.AddWithValue("@CustomerPhone", model.Mobile);
                cmd.Parameters.AddWithValue("@CustomerEmail", model.Email);
                cmd.Parameters.AddWithValue("@CustomerDOB", model.DateOfBirth);
                //twsttt
                cmd.Parameters.AddWithValue("@AccountType", model.AccountType);
                cmd.Parameters.AddWithValue("@AccountNo", model.AccountNo);
                cmd.Parameters.AddWithValue("@CreatedBy", model.Name);
                cmd.Parameters.AddWithValue("@CreatorRole", model.ROLE);
                cmd.Parameters.AddWithValue("@Branch", model.BRANCH);

                //added
                cmd.Parameters.AddWithValue("@CreatorID", model.Data1);
                cmd.Parameters.AddWithValue("@TApproverName", model.Approver);
                cmd.Parameters.AddWithValue("@TAmount", model.Amt);
                cmd.Parameters.AddWithValue("@TStatus", model.Status1);
                cmd.Parameters.AddWithValue("@TID", model.Data2);
                





               // model.Mobile = model.Mobile;
              
                //


                using SqlDataAdapter da = new(cmd);
                DataTable dt = new();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during data retrieval: " + ex.Message);
                    throw;
                }

                return dt;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
        #endregion

        #region TEST
        public DataTable DOTEST(UserViewModel model, string activity, string storedProcedureName)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                con.Open();
                using SqlCommand cmd = new(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Activity ", activity);
                cmd.Parameters.AddWithValue("@Data1", model.Data1);
                cmd.Parameters.AddWithValue("@Data2", model.Data2);
                cmd.Parameters.AddWithValue("@Data3", model.Data3);
                cmd.Parameters.AddWithValue("@Data4", model.Data4);
                cmd.Parameters.AddWithValue("@Data5", model.Data5);
                cmd.Parameters.AddWithValue("@Data6", model.Data6);
                cmd.Parameters.AddWithValue("@Data7", model.Data7);




                using SqlDataAdapter da = new(cmd);
                DataTable dt = new();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during data retrieval: " + ex.Message);
                    throw;
                }

                return dt;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        #endregion


        #region check username and mail
        public bool CheckUsernameExistence(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) // Check if username is null or empty
                {
                    return false; // Return false if username is empty
                }

                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                con.Open();
                string query = "SELECT CASE WHEN EXISTS (SELECT 1 FROM UserDetails WHERE UserName = @Username) THEN 1 ELSE 0 END";
                using SqlCommand cmd = new(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                int result = (int)cmd.ExecuteScalar();
                return result == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking username existence: " + ex.Message);
                throw;
            }
        }




        public bool CheckEmailExistence(string email)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);

                if (string.IsNullOrEmpty(email)) // Check if username is null or empty
                {
                    return false; // Return false if username is empty
                }
                con.Open();
                string query = "SELECT COUNT(*) FROM UserDetails WHERE Email = @Email";
                using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                object result = (int)cmd.ExecuteScalar();
                int count = Convert.ToInt32(result ?? 0);
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking email existence: " + ex.Message);
                throw;
            }
        }


        #endregion
    }
}
