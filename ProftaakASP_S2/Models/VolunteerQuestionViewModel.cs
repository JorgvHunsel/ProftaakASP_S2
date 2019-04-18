using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class VolunteerQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Urgency { get; set; }
        public string Category { get; set; }
        public string CareRecipientName { get; set; }

        public VolunteerQuestionViewModel(Question question, User volunteer)
        {
            QuestionId = question.QuestionId;
            Title = question.Title;
            Content = question.Content;
            Status = question.Status.ToString();
            Date = question.Date;
            Urgency = question.Urgency;
            Category = question.Category.Name;
            CareRecipientName = volunteer.FirstName;
        }

        public VolunteerQuestionViewModel()
        {
            
        }
    }
}
