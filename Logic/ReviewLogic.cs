using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class ReviewLogic
    {
        private readonly IReviewContext _reviewContext;

        public ReviewLogic(IReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public void InsertReview(ReviewInfo review)
        {
            _reviewContext.InsertReview(review);
        }
    }
}