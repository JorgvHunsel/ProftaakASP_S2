using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;


namespace Logic
{
    public class QuestionLogic
    {
        private readonly IQuestionContext _question;

        public QuestionLogic(IQuestionContext question)
        {
            _question = question;
        }

        public void WriteQuestionToDatabase(Question askedQuestion)
        {
            if(askedQuestion.Title == "")
                throw new ArgumentException("Title can't be empty");
            if (askedQuestion.Title.Length > 100)
                throw new ArgumentException("Title can't be too long");

            if (askedQuestion.Content == "")
                throw new ArgumentException("Content can't be empty");
            if (askedQuestion.Content.Length > 500)
                throw new ArgumentException("Content can't be too long");

            _question.WriteQuestionToDatabase(askedQuestion);
        }

        public void DeleteQuestionFromDatabase(Question askedQuestion)
        {
            _question.DeleteQuestionFromDatabase(askedQuestion);
        }

        public List<Question> GetAllQuestionsProfessional(int userid, string statusrequest)
        {
            return _question.GetAllQuestionsProfessional(userid, statusrequest);
        }

        public List<Question> GetAllOpenQuestions()
        {
            return _question.GetAllOpenQuestions();
        }

        public List<Question> GetAllOpenQuestionCareRecipientID(int careRecipientID) =>
            _question.GetAllOpenQuestionsCareRecipientID(careRecipientID);

        public Question GetSingleQuestion(int questionID)
        {
            return _question.GetSingleQuestion(questionID);
        }

        public void EditQuestion(Question question)
        {
            _question.EditQuestion(question);
        }

        public void ChangeStatus(int id, string status)
        {
            _question.ChangeQuestionStatus(id, status == "Open" ? "Closed" : "Open");
        }

        public List<Question> GetAllClosedQuestionsCareRecipientId(int careRecipient)
        {
            return _question.GetAllClosedQuestionsCareRecipientID(careRecipient);
        }

        public List<Question> GetAllQuestions()
        {
            return _question.GetAllQuestions();
        }
    
    }
}
