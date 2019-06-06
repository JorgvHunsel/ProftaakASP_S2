using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class ReviewContextMock : IReviewContext
    {
        public void InsertReview(ReviewInfo review)
        {
            
        }

        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
            return new List<ReviewInfo>();
        }
    }
}
