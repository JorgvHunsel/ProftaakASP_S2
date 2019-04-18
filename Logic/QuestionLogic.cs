﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            _question.WriteQuestionToDatabase(askedQuestion);
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

        public void EditQuestion(int questionID, string subjectNew, string contentNew, Category category, string urgency)
        {
            _question.EditQuestion(questionID, subjectNew, contentNew, category, urgency);
        }

    }
}