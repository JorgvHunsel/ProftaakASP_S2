using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Models;


namespace Logic
{
    public class QuestionLogic
    {
        static QuestionContextSQL questionRepo = new QuestionContextSQL();

        public static void WriteQuestionToDatabase(Question askedQuestion)
        {
            questionRepo.WriteQuestionToDatabase(askedQuestion);
        }

        public List<Question> GetAllOpenQuestions()
        {
            return questionRepo.GetAllOpenQuestions();
        }

        public static List<Question> GetAllOpenQuestionCareRecipientID(int careRecipientID) =>
            questionRepo.GetAllOpenQuestionsCareRecipientID(careRecipientID);

        public static Question GetSingleQuestion(int questionID)
        {
            return questionRepo.GetSingleQuestion(questionID);
        }

        public static void EditQuestion(int questionID, string subjectNew, string contentNew, Category category, string urgency)
        {
            questionRepo.EditQuestion(questionID, subjectNew, contentNew, category, urgency);
        }

    }
}
