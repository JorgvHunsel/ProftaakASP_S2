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
        List<Question> GetAllOpenQuestions();
        Question GetSingleQuestion(int questionID);
        void EditQuestion(Question question);
        List<Question> GetAllOpenQuestionsCareRecipientID(int careRecipientID);
    }
}
