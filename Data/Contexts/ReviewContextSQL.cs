using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Data.Interfaces;
using Models;
using System.Globalization;
using System.Text.RegularExpressions;

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
                string query =
                    "INSERT INTO [VolunteerReview] (CareRecipientID, VolunteerID, Content, Rating, RatingTime) VALUES (@recipientId, @volunteerId, @content, @rating, @ratingTime)";
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<ReviewInfo> GetAllReviews()
        {
            try
            {
                List<ReviewInfo> reviewList = new List<ReviewInfo>();

                SqlCommand cmd = new SqlCommand("GetAllReviews", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int reviewId = Convert.ToInt32(dr["Review_ID"]);
                    int volunteerId = Convert.ToInt32(dr["VolunteerID"]);
                    string volunteerFirstName = dr["VFName"].ToString();
                    string volunteerLastName = dr["VLName"].ToString();
                    string careFirstName = dr["CFName"].ToString();
                    string careLastName = dr["CLName"].ToString();
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    string reviewContent = dr["Content"].ToString();
                    int starAmount = Convert.ToInt32(dr["Rating"]);
                    DateTime dateTime = Convert.ToDateTime(dr["RatingTime"]);

                    ReviewInfo review = new ReviewInfo(reviewId, volunteerId, careRecipientId, reviewContent,
                        starAmount, volunteerFirstName, volunteerLastName, careFirstName, careLastName);
                    reviewList.Add(review);
                }

                return reviewList;
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
                throw e;
            }
            finally
            {
                _conn.Close();
                
            }
        }

        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
            List<ReviewInfo> reviews = new List<ReviewInfo>();
            try
            {
                string query = "SELECT * FROM [VolunteerReview] WHERE [VolunteerID] = @VolunteerId";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.Add("@VolunteerId", SqlDbType.Int).Value = volunteerId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int careRecipientId = Convert.ToInt32(row["CareRecipientID"]);
                    string review = Convert.ToString(row["Content"]);
                    int starAmount = Convert.ToInt32(row["Rating"]);

                    reviews.Add(new ReviewInfo(volunteerId, careRecipientId, review, starAmount));
                }

                return reviews;
            }
            finally
            {
                _conn.Close();
            }

        }
    }
}