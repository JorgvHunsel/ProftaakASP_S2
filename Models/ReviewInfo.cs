using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ReviewInfo
    {
        public int ReviewId { get; set; }
        public int VolunteerId { get; set; }
        public int CareRecipientId { get; set; }
        public string Review { get; set; }
        public int StarAmount { get; set; }

        public ReviewInfo(int reviewId, int volunteerId, int careRecipientId, string review, int starAmmount)
        {
            ReviewId = reviewId;
            VolunteerId = volunteerId;
            CareRecipientId = careRecipientId;
            Review = review;
            StarAmount = starAmmount;
        }
    }
}
