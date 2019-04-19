using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class ReviewContextSQL : IReviewContext
    {
        private const string ConnectionString =
            @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly SqlConnection _conn = new SqlConnection(ConnectionString);


        public void InsertReview(ReviewInfo review)
        {
            try
            {
                _conn.Open();
                string query = "INSERT INTO [VolunteerReview] (CareRecipientID, VolunteerID, Content, Rating, RatingTime) VALUES (@recipientId, @volunteerId, @content, @rating, @ratingTime)";
                using (SqlCommand insertReview = new SqlCommand(query, _conn))
                {
                    insertReview.Parameters.AddWithValue("@recipientId", review.CareRecipientId);
                    insertReview.Parameters.AddWithValue("@volunteerId", review.VolunteerId);
                    insertReview.Parameters.AddWithValue("@content", review.Review ?? "");
                    insertReview.Parameters.AddWithValue("@rating", review.StarAmount);
                    insertReview.Parameters.AddWithValue("@ratingTime", DateTime.Now);
                    insertReview.ExecuteNonQuery();
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
