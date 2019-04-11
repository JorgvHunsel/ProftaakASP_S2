using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
{
    public interface IQuestionContext
    {
        void WriteQuestionToDatabase(Question askedQuestion);
        DataTable GetAllOpenQuestions();
        Question GetSingleQuestion(int questionID);
        void EditQuestion(int questionID, string subjectNew, string contentNew, Category category, string urgency);
        List<Question> GetAllOpenQuestionsCareRecipientID(int careRecipientID);
    }
}
