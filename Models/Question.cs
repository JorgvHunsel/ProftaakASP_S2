using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontys.PTS2.Prototype.Classes
{
    public class Question
    {
        public enum QuestionStatus { Open, Closed}
        public int QuestionId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public QuestionStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string Urgency { get; set; }
        public Category Category { get; private set; }
        public int CareRecipientId { get; private set; }

        public Question(int questionId, string title, string content, QuestionStatus status, DateTime date, string urgency, Category category, int careRecipientID)
        {
            QuestionId = questionId;
            Title = title;
            Content = content;
            Status = status;
            Date = date;
            Urgency = urgency;
            Category = category;
            CareRecipientId = careRecipientID;
        }

        public Question(string title, string content, QuestionStatus status, DateTime date, string urgency, Category category, int careRecipientID)
        {
            Title = title;
            Content = content;
            Status = status;
            Date = date;
            Urgency = urgency;
            Category = category;
            CareRecipientId = careRecipientID;
        }

        public void PostQuestion(Question askedQuestion)
        {

        }
        public void DeleteQuestion(Question unwantedQuestion)
        {

        }

        public void EditQuestion(Question editQuestion)
        {

        }

        public DataTable GetAllQuestions()
        {
            return null;
        }

        public override string ToString()
        {
            return $" '{Status}', '{Title}', '{Content}', '{Date.ToString("yyyy-M-d hh:mm tt")}', '{Urgency}', '{Category.CategoryID}' ";
        }
    }
}
