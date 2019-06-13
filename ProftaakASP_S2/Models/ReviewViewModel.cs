using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class ReviewViewModel
    {
        public string CareRecipientName { get; set; }
        public string VolunteerName { get; set; }
        public int ReviewId { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        [Required]
        public int CareRecipientId { get; set; }
        [Required]
        public string Review { get; set; }
        [Required]
        public int starAmount { get; set; }
        public int QuestionID { get; set; }

        public ReviewViewModel(ReviewInfo review)
        {
            ReviewId = review.ReviewId;
            VolunteerId = review.VolunteerId;
            CareRecipientId = review.CareRecipientId;
            Review = review.Review;
            starAmount = review.StarAmount;
            VolunteerName = review.VolFirstName + " " + review.VolLastName;
            CareRecipientName = review.CareFirstName + " " + review.CareLastName;
        }

        public ReviewViewModel(int volunteerId, int questionId)
        {
            VolunteerId = volunteerId;
            QuestionID = questionId;
        }
        public ReviewViewModel()
        {

        }
    }
}