using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class LogContextSQL : ILogContext
    {
        private const string ConnectionString = @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection _conn = new SqlConnection(ConnectionString);

        public void CreateUserLog(int userId, User user)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddUserLog", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Title", user.Status ? "Unblock user" : "Block user");
                    cmd.Parameters.AddWithValue("@Description", $"User: {user.FirstName} ,{user.UserId}");
                    cmd.Parameters.AddWithValue("@Datetime", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
