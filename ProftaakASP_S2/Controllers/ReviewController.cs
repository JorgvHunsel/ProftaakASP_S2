﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class ReviewController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;
        private readonly ReviewLogic _reviewLogic;

        public ReviewController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic, ReviewLogic reviewLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
            _reviewLogic = reviewLogic;
        }

        [HttpPost]
        public ActionResult SubmitReview(ReviewViewModel reviewViewModel)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            _reviewLogic.InsertReview(new ReviewInfo(reviewViewModel.VolunteerId, userId, reviewViewModel.Review, reviewViewModel.starAmount));

            return View("../Shared/Login");
        }

        [HttpPost]
        public ActionResult DeleteReview(int reviewId)
        {
            _reviewLogic.DeleteReview(reviewId);

            return RedirectToAction("ReviewOverview", "Admin");
        }

    }
}