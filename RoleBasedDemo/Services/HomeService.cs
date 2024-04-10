
using RoleBasedDemo.Helpers;
using RoleBasedDemo.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;


namespace RoleBasedDemo.Services
{
    public interface IHomeService
    {
        
        DataTable User_Login(LoginViewModel model);
        
    }

    public class HomeService : IHomeService
    {
        public ConnectionStrings _connectionStrings;
        private SqlConnection Con { get; set; }
        private SqlCommand Cmd { get; set; }

        public HomeService(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }
        #region LOGIN
        public DataTable User_Login(LoginViewModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionStrings.SQL_Connection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("CheckUserCredentials", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTIVITY", model.Activity);
                        cmd.Parameters.AddWithValue("@p_username", model.UserName);
                        cmd.Parameters.AddWithValue("@p_password", model.PassWord);

                       
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
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
                    }
                }
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