using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Logic
{
    public class QuestionLogic
    {
        static QuestionRepository questionRepo = new QuestionRepository();

        public static void WriteQuestionToDatabase(Question askedQuestion)
        {
            questionRepo.WriteQuestionToDatabase(askedQuestion);
        }

        public static DataTable GetAllOpenQuestions()
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

        public static List<ListViewItem> LoadQuestionsToList(List<Question> questionList)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            foreach (Question question in questionList)
            {
                //Standard adding item, only add the primary key as main item
                ListViewItem item = new ListViewItem(question.QuestionId.ToString());
                item.SubItems.Add(question.Title);
                item.SubItems.Add(question.Content);
                item.SubItems.Add(question.Status.ToString());
                item.SubItems.Add(question.Date.ToString());
                item.SubItems.Add(question.Urgency);
                item.SubItems.Add(question.Category.ToString());
                items.Add(item);
            }
            return items;
        }
    }
}
