using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class ReviewTests
    {
        private ReviewLogic _reviewLogic;
        private ReviewContextMock _reviewContext;

        [TestInitialize]
        public void TestInitializer()
        {
            _reviewContext = new ReviewContextMock();
            _reviewLogic = new ReviewLogic(_reviewContext);
        }

        [TestMethod]
        public void InsertReview()
        {
            Assert.AreEqual(0, _reviewLogic.GetAllReviewsWithVolunteerId(1).Count);

            ReviewInfo reviewInfo = new ReviewInfo(1, 1, 1, "test", 5);
            _reviewLogic.InsertReview(reviewInfo);
            Assert.AreEqual(1, _reviewLogic.GetAllReviewsWithVolunteerId(1).Count);
        }

        [TestMethod]
        public void GetAllReviews()
        {
            Assert.AreEqual(0, _reviewLogic.GetAllReviews().Count);

            ReviewInfo reviewInfo1 = new ReviewInfo(1, 1, 1, "test1", 5);
            _reviewLogic.InsertReview(reviewInfo1);
            Assert.AreEqual(1, _reviewLogic.GetAllReviews().Count);

            _reviewLogic.InsertReview(reviewInfo1);
            _reviewLogic.InsertReview(reviewInfo1);
            _reviewLogic.InsertReview(reviewInfo1);
            _reviewLogic.InsertReview(reviewInfo1);

            Assert.AreEqual(5, _reviewLogic.GetAllReviews().Count);
        }

        [TestMethod]
        public void DeleteReview()
        {
            
            ReviewInfo reviewInfo1 = new ReviewInfo(1, 1, 1, "test1", 5);
            ReviewInfo reviewInfo2 = new ReviewInfo(2, 2, 2, "test2", 5);
            ReviewInfo reviewInfo3 = new ReviewInfo(3, 3, 3, "test3", 5);

            Assert.AreEqual(0, _reviewLogic.GetAllReviews().Count);

            _reviewLogic.InsertReview(reviewInfo1);
            _reviewLogic.InsertReview(reviewInfo2);
            _reviewLogic.InsertReview(reviewInfo3);

            Assert.AreEqual(3, _reviewLogic.GetAllReviews().Count);

            _reviewLogic.DeleteReview(1);

            Assert.AreEqual(0,_reviewLogic.GetAllReviewsWithVolunteerId(1).Count);

            Assert.AreEqual(2, _reviewLogic.GetAllReviews().Count);
        }
    }
}
