using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProftaakASP_S2.Models
{
    public class ChatViewModel
    {
        public int ChatLogID { get; set; }
        public string VolunteerName { get; set; }
        public string CareRecipientName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string QuestionName { get; set; }
        public int QuestionId { get; set; }
        public int ReactionId { get; set; }
        public int VolunteerId { get; set; }
        public int CareRecipientId { get; set; }
    }
}
