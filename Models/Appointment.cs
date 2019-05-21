using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Appointment
    {
        public int AppointmentId { get; private set; }
        public int QuestionId { get; private set; }
        public int CareRecipientId { get; private set; }
        public int VolunteerId { get; private set; }
        public DateTime TimeStampCreation { get; private set; } 
        public DateTime TimeStampAppointment { get; private set; }
        public string Location { get; private set; }

        public Appointment(int appointmentId, int questionId, int careRecipientId, int volunteerId, DateTime timeStampCreation, DateTime timeStampAppointment, string location)
        {
            AppointmentId = appointmentId;
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStampCreation = timeStampCreation;
            TimeStampAppointment = timeStampAppointment;
            Location = location;
        }

        public Appointment(int questionId, int careRecipientId, int volunteerId, DateTime timeStampCreation, DateTime timeStampAppointment, string location)
        {
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStampCreation = timeStampCreation;
            TimeStampAppointment = timeStampAppointment;
            Location = location;
        }

        public Appointment(int questionId, int careRecipientId, int volunteerId, string location)
        {
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            Location = location;
        }
    }
}
