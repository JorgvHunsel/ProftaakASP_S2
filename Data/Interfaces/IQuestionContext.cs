using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;

namespace Fontys.PTS2.Prototype.Data.Contexts
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
