using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class ReviewContextMock : IReviewContext
    {
        public List<ReviewInfo> ReviewList = new List<ReviewInfo>();
        public void InsertReview(ReviewInfo review)
        {
            ReviewList.Add(review);
        }

        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
           return ReviewList.FindAll(r => r.VolunteerId == volunteerId);
        }

        public List<ReviewInfo> GetAllReviews()
        {
            return ReviewList;
        }

        public void DeleteReview(int reviewId)
        {
            ReviewList.RemoveAll(x => x.ReviewId == reviewId);
        }
    }
}
