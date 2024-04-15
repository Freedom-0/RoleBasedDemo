
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RoleBasedDemo.Services { 

    public interface IRegisterService
    {
           bool RegisterUser(RegisterViewModel model);
           DataTable GetUserDetails(UserViewModel model,string activity, string storedProcedureName/*, Dictionary<string, object> parameters*/);

    }

    public class RegisterService : IRegisterService
    {
        private readonly ConnectionStrings _connectionStrings;
        
        public RegisterService(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }


        #region registeruser
        public bool RegisterUser(RegisterViewModel model)
        {
            try
            {
                using SqlConnection con = new SqlConnection(_connectionStrings.SQL_Connection);
                con.Open();
                using SqlCommand cmd = new SqlCommand("DEMO_SP", con); // Assuming you have a stored procedure named RegisterUser
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTIVITY", "RegisterUser"); // Pass the activity parameter
                cmd.Parameters.AddWithValue("@user", model.Username);
                cmd.Parameters.AddWithValue("@fullname", model.FullName);
                cmd.Parameters.AddWithValue("@email", model.Email);
                cmd.Parameters.AddWithValue("@userpass", model.Password);



                using SqlDataAdapter da = new(cmd);
                DataTable dt = new();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    Console.WriteLine("Error during data retrieval: " + ex.Message);
                  
                    throw;
                }

                return true;

            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error during user registration: " + ex.Message);
               
                throw;
            }
        }
        #endregion

        #region reusable datatable
        public DataTable GetUserDetails(UserViewModel model, string activity, string storedProcedureName/*, Dictionary<string, object> parameters*/)
        {
            try
            {
                using SqlConnection con = new(_connectionStrings.SQL_Connection);
                con.Open();
                using SqlCommand cmd = new(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTIVITY", activity);
                cmd.Parameters.AddWithValue("@user", model.Name);
                cmd.Parameters.AddWithValue("@userpass", model.Code);

                // Add additional parameters if provided
                //if (parameters != null)
                //{
                //    foreach (var parameter in parameters)
                //    {
                //        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                //    }
                //}

                using SqlDataAdapter da = new(cmd);
                DataTable dt = new();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    Console.WriteLine("Error during data retrieval: " + ex.Message);
                    // Re-throw the exception to propagate it further
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
    }
}
