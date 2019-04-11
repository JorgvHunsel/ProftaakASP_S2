using System;
using System.Data;
using System.Data.SqlClient;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data.Contexts;
using Fontys.PTS2.Prototype.Model;

namespace Fontys.PTS2.Prototype.Data
{
    public class AppointmentContextSQL : IAppointmentContext
    {
        private const string ConnectionString = @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly SqlConnection _conn = new SqlConnection(ConnectionString);

        public void CreateAppointment(Appointment appointment)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CreateAppointment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@questionID", appointment.QuestionId);
                    cmd.Parameters.AddWithValue("@careRecipientID", appointment.CareRecipientId);
                    cmd.Parameters.AddWithValue("@volunteerID", appointment.VolunteerId);
                    cmd.Parameters.AddWithValue("@timestampAppointment", appointment.TimeStamp);

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