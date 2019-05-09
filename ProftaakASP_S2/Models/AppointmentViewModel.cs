using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class AppointmentViewModel
    {
        public int QuestionId { get; set; }
        public int CareRecipientId { get; set; }
        public int VolunteerId { get; set; }
        public DateTime TimeStamp { get; set; }

        public AppointmentViewModel(Appointment appointment)
        {
            QuestionId = appointment.QuestionId;
            CareRecipientId = appointment.CareRecipientId;
            VolunteerId = appointment.VolunteerId;
            TimeStamp = appointment.TimeStamp;
        }

        public AppointmentViewModel(int questionId, int careRecipientId, int volunteerId)
        {
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
        }

        public AppointmentViewModel()
        {
            
        }
    }
}
