using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.Win32.SafeHandles;
using Models;

namespace Data.Contexts
{
    public class QuestionContextSQL : IQuestionContext
    {
        private const string ConnectionString =
            @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly SqlConnection _conn = new SqlConnection(ConnectionString);

        public void WriteQuestionToDatabase(Question askedQuestion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = askedQuestion.Status;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = askedQuestion.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = askedQuestion.Content;
                cmd.Parameters.Add("@urgency", SqlDbType.NVarChar).Value = askedQuestion.Urgency;
                cmd.Parameters.Add("@categoryID", SqlDbType.Int).Value = askedQuestion.Category.CategoryId;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = askedQuestion.CareRecipientId;
                cmd.Parameters.Add("@datetime", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-M-d hh:mm tt");


                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Question> GetAllOpenQuestions()
        {
            try
            {
                List<Question> questionList = new List<Question>();

                SqlCommand cmd = new SqlCommand("SelectAllOpenQuestions", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"].ToString());
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"].ToString());
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(0, dr["Name"].ToString(), null);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"].ToString());
                    string status = dr["Status"].ToString();

                    Question question;
                    if (status == "Open")
                    {
                        question = new Question(questionId, title, content, Question.QuestionStatus.Open, date, urgency, category, careRecipientId);
                    }
                    else
                    {
                        question = new Question(questionId, title, content, Question.QuestionStatus.Closed, date, urgency, category, careRecipientId);
                    }
                    questionList.Add(question);
                    
                }

                return questionList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Question> GetAllOpenQuestionsCareRecipientID(int careRecipientID)
        {
            List<Question> questionList = new List<Question>();

            try
            {
                SqlCommand cmd = new SqlCommand("SelectAllOpenQuestionsCareRecipientID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = careRecipientID;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    Question.QuestionStatus status = Question.QuestionStatus.Open;
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"].ToString());
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    Category category = new Category(dr["Name"].ToString());
                    questionList.Add(new Question(questionId, title, content, status, date, urgency, category, careRecipientId));
                }
                return questionList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public Question GetSingleQuestion(int questionID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetQuestionById", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = questionID;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                int CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryId"].ToString());
                string CategoryName = (dt.Rows[0]["Name"].ToString());
                string CategoryDescription = (dt.Rows[0]["CDescription"].ToString());
                int CareRecipientID = Convert.ToInt32(dt.Rows[0]["CareRecipientID"].ToString());

                int QuestionID = Convert.ToInt32(dt.Rows[0]["QuestionID"].ToString());
                string title = (dt.Rows[0]["Title"].ToString());
                string content = (dt.Rows[0]["QDescription"].ToString());
                bool urgency = Convert.ToBoolean(dt.Rows[0]["Urgency"]);

                DateTime timeStamp = Convert.ToDateTime(dt.Rows[0]["TimeStamp"].ToString());

                Category category = new Category(CategoryID, CategoryName, CategoryDescription);
                Question question = new Question(QuestionID, title, content, Question.QuestionStatus.Open, timeStamp, urgency, category, CareRecipientID);
                return question;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void EditQuestion(Question question)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("EditQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = question.Category.CategoryId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = question.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = question.Content;
                cmd.Parameters.Add("@urgency", SqlDbType.Bit).Value = question.Urgency;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = question.QuestionId;


                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;

            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
