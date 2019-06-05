using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IReviewContext
    {
        void InsertReview(ReviewInfo review);
    }
}