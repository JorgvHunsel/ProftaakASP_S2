using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontys.PTS2.Prototype.Model
{
    public class Appointment
    {
        public int QuestionId { get; private set; }
        public int CareRecipientId { get; private set; }
        public int VolunteerId { get; private set; }
        public DateTime TimeStamp { get; private set; } 

        public Appointment(int questionID, int careRecipientId, int volunteerId, DateTime timeStamp)
        {
            QuestionId = questionID;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStamp = timeStamp;
        }
    }
}
