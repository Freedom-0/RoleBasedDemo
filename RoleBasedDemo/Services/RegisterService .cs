
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


//namespace RoleBasedDemo.Services
//{
//    public interface IRegisterService
//    {

//        DataTable Registration(LoginViewModel model);

//    }

//    public class RegisterService : IRegisterService
//    {
//        public ConnectionStrings _connectionStrings;
//        private SqlConnection Con { get; set; }
//        private SqlCommand Cmd { get; set; }

//        public RegisterService(IOptions<ConnectionStrings> options)
//        {
//            _connectionStrings = options.Value;
//        }
//        #region LOGIN
//        public DataTable Registration(LoginViewModel model)
//        {
//            try
//            {
//                using (SqlConnection con = new SqlConnection(_connectionStrings.SQL_Connection))
//                {
//                    con.Open();
//                    using (SqlCommand cmd = new SqlCommand("CheckUserCredentials", con))
//                    {
//                        cmd.CommandType = CommandType.StoredProcedure;
//                        cmd.Parameters.AddWithValue("@ACTIVITY", model.Activity);
//                        cmd.Parameters.AddWithValue("@p_username", model.UserName);
//                        cmd.Parameters.AddWithValue("@p_password", model.PassWord);


//                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//                        {
//                            DataTable dt = new DataTable();
//                            try
//                            {
//                                da.Fill(dt);
//                            }
//                            catch (Exception ex)
//                            {
//                                // Log the exception for debugging purposes
//                                Console.WriteLine("Error during data retrieval: " + ex.Message);
//                                // Re-throw the exception to propagate it further
//                                throw;
//                            }

//                            return dt;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle the exception
//                Console.WriteLine("Error: " + ex.Message);
//                throw;
//            }
//        }

//        #endregion 


//    }
//}


// IRegisterService.cs




namespace RoleBasedDemo.Services { 

    public interface IRegisterService
    {
           bool RegisterUser(RegisterViewModel model);
    }

    public class RegisterService : IRegisterService
    {
        private readonly ConnectionStrings _connectionStrings;

        public RegisterService(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }

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
                    // Re-throw the exception to propagate it further
                    throw;
                }

                return true;

                //int rowsAffected = cmd.ExecuteNonQuery();

                //return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine("Error during user registration: " + ex.Message);
                // Optionally, you can throw the exception to propagate it further
                throw;
            }
        }
    }
}
