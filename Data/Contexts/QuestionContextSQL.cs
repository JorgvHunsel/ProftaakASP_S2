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

        //public List<Question> GetAllQuestions()
        //{
        //    try
        //    {
        //        string query =
        //            "SELECT QuestionID, CareRecipientID, CategoryId, Status, Title, Description, Datetime, Urgency " +
        //            "FROM Question";
        //        _conn.Open();
        //        SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, _conn);

        //        DataTable dt = new DataTable();
        //        sqlAdapter.Fill(dt);

        //        List<Question> QuestionList = new List<Question>();

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            int questionId = (int)row["QuestionID"];
        //            int careRecipientID = (int)row["CareRecipientID"];
        //            int categoryId = (int)row["CategoryId"];
        //            string status = row["Status"].ToString();
        //            string title = row["Title"].ToString();
        //            string description = row["Description"].ToString();
        //            DateTime Datetime = (DateTime)row["Urgency"];
        //            string urgency = row["Urgency"].ToString();


        //        }

        //        return QuestionList;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        _conn.Close();
        //    }
        //}

        //public string GetQuestionName(int questionId)
        //{
        //    try
        //    {
        //        string query = "SELECT [Title] FROM [Question] WHERE [QuestionID] = " + questionId.ToString();
        //        _conn.Open();
        //        SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, _conn);

        //        DataTable dt = new DataTable();
        //        sqlAdapter.Fill(dt);

        //        string questionTitle = "";

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            questionTitle = row["Title"].ToString();
        //        }

        //        return questionTitle;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        _conn.Close();
        //    }
        //}

        //public string GetQuestionDescription(int questionId)
        //{
        //    try
        //    {
        //        string query = "SELECT [Description] FROM [Question] WHERE [QuestionID] = " + questionId.ToString();
        //        _conn.Open();
        //        SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, _conn);

        //        DataTable dt = new DataTable();
        //        sqlAdapter.Fill(dt);

        //        string description = "";

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            description = row["Description"].ToString();
        //        }

        //        return description;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        _conn.Close();
        //    }
        //}

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
                cmd.Parameters.Add("@categoryID", SqlDbType.Int).Value = askedQuestion.Category.CategoryID;
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

        public DataTable GetAllOpenQuestions()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SelectAllOpenQuestions", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                return dt;

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
                    string urgency = dr["Urgency"].ToString();
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
                string urgency = (dt.Rows[0]["Urgency"].ToString());

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

        public void EditQuestion(int questionID, string subjectNew, string contentNew, Category category, string urgency)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("EditQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = category.CategoryID;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = subjectNew;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = contentNew;
                cmd.Parameters.Add("@urgency", SqlDbType.NVarChar).Value = urgency;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = questionID;

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

        //public static void EditQuestion(int questionID, string subjectNew, string contentNew, string urgency)
        //{
        //    try
        //    {
        //        _conn.Open();
        //        SqlCommand cmd = new SqlCommand
        //        {

        //            CommandType = System.Data.CommandType.Text,
        //            CommandText =
        //                "UPDATE Question SET Title = '" + subjectNew + "', Description = '" + contentNew + "', Urgency = '" + urgency + "' WHERE QuestionID = '" + questionID + "'"
        //        };

        //        cmd.Connection = _conn;
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //    finally
        //    {
        //        _conn.Close();
        //    }
        //}
    }
}
