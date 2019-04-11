using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data.Contexts;

namespace Fontys.PTS2.Prototype.Data
{
    class ChatContextSQL : IChatContext
    {
        private const string ConnectionString = @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection _conn = new SqlConnection(ConnectionString);

        public List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid)
        {
            List<ChatLog> chatLogList = new List<ChatLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetChatsByVolunteerID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatLogID = Convert.ToInt32(dr["ChatLogID"].ToString());
                    string questionTitle = (dr["Title"].ToString());
                    int careRecipientID = Convert.ToInt32(dr["CareRecipientID"].ToString());
                    int volunteerID = Convert.ToInt32(dr["VolunteerID"].ToString());
                    string careRecipientFirstName = dr["CareRecipientFirstName"].ToString();
                    string careRecipientLastName = dr["CareRecipientLastName"].ToString();
                    int questionID = Convert.ToInt32(dr["QuestionID"].ToString());

                    string volunteerFirstName = dr["VolunteerFirstName"].ToString();
                    string volunteerLastName = dr["VolunteerLastName"].ToString();

                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());


                    ChatLog chatLog = new ChatLog(chatLogID, questionTitle, careRecipientID, volunteerID, careRecipientFirstName, careRecipientLastName, volunteerFirstName, volunteerLastName, timeStamp, questionID);
                    chatLogList.Add(chatLog);
                }

                return chatLogList;
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

        public List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid)
        {
            List<ChatLog> chatLogList = new List<ChatLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllChatsByCareRecipientID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatLogID = Convert.ToInt32(dr["ChatLogID"].ToString());
                    string questionTitle = (dr["Title"].ToString());
                    int careRecipientID = Convert.ToInt32(dr["CareRecipientID"].ToString());
                    int volunteerID = Convert.ToInt32(dr["VolunteerID"].ToString());
                    string careRecipientFirstName = dr["CareRecipientFirstName"].ToString();
                    string careRecipientLastName = dr["CareRecipientLastName"].ToString();
                    int questionID = Convert.ToInt32(dr["QuestionID"].ToString());

                    string volunteerFirstName = dr["VolunteerFirstName"].ToString();
                    string volunteerLastName = dr["VolunteerLastName"].ToString();

                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());


                    ChatLog chatLog = new ChatLog(chatLogID, questionTitle, careRecipientID, volunteerID, careRecipientFirstName, careRecipientLastName, volunteerFirstName, volunteerLastName, timeStamp, questionID);
                    chatLogList.Add(chatLog);
                }

                return chatLogList;
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

        public List<ChatMessage> LoadMessageAsListUsingChatLogID(int chatID)
        {
            List<ChatMessage> chatMessageList = new List<ChatMessage>();
            try
            {
                SqlCommand cmd = new SqlCommand("LoadMessagesByChatLogID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatid", SqlDbType.Int).Value = chatID;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatlogID = Convert.ToInt32(dr["ChatID"].ToString());
                    string content = (dr["Content"].ToString());

                    int senderID = Convert.ToInt32(dr["SenderID"].ToString());
                    int receiverID = Convert.ToInt32(dr["ReceiverID"].ToString());

                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());

                    ChatMessage chatMessage = new ChatMessage(chatlogID, receiverID, senderID, content, timeStamp);
                    chatMessageList.Add(chatMessage);
                }

                return chatMessageList;

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

        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertMessage", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatid", SqlDbType.Int).Value = chatid;
                cmd.Parameters.Add("@senderid", SqlDbType.Int).Value = senderid;
                cmd.Parameters.Add("@receiverid", SqlDbType.Int).Value = receiverid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CreateNewChatLog", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reactionID", SqlDbType.Int).Value = reactionID;
                cmd.Parameters.Add("@volunteerID", SqlDbType.Int).Value = volunteerID;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = careRecipientID;

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

        //Wesley
        public List<ChatLog> LoadOpenChatsList()
        {
            List<ChatLog> openChatsList = new List<ChatLog>();
            try
            {
                //SqlCommand cmd = new SqlCommand("GetAllChatsByCareRecipientID", _conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;

                //_conn.Open();

                //DataTable dt = new DataTable();
                //dt.Load(cmd.ExecuteReader());

                //foreach (DataRow dr in dt.Rows)
                //{
                //    int chatLogID = Convert.ToInt32(dr["ChatLogID"].ToString());
                //    string questionTitle = (dr["Title"].ToString());
                //    int careRecipientID = Convert.ToInt32(dr["CareRecipientID"].ToString());
                //    int volunteerID = Convert.ToInt32(dr["VolunteerID"].ToString());
                //    string careRecipientFirstName = dr["CareRecipientFirstName"].ToString();
                //    string careRecipientLastName = dr["CareRecipientLastName"].ToString();
                //    int questionID = Convert.ToInt32(dr["QuestionID"].ToString());
                //    string volunteerFirstName = dr["VolunteerFirstName"].ToString();
                //    string volunteerLastName = dr["VolunteerLastName"].ToString();
                //    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());


                //    ChatLog chatLog = new ChatLog(chatLogID, questionTitle, careRecipientID, volunteerID, careRecipientFirstName, careRecipientLastName, volunteerFirstName, volunteerLastName, timeStamp, questionID);
                //    openMessageList.Add(chatLog);
                //}

                return openChatsList;
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
        //End Wesley


    }
}
