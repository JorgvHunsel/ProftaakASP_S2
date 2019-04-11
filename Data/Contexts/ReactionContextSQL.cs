using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data.Contexts;

namespace Fontys.PTS2.Prototype.Data
{
    public class ReactionContextSQL : IReactionContext
    {
        private const string ConnectionString = @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly SqlConnection _conn = new SqlConnection(ConnectionString);
        //er mogen hier geen static methodes voorkomen, deze moeten pas static worden in de logic layer.
        public void PostReaction(Reaction reaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertReaction", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@senderid", SqlDbType.Int).Value = reaction.SenderId;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = reaction.QuestionId;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = reaction.Description;

                _conn.Open();
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

        public List<Reaction> GetAllCommentsWithQuestionID(int questionId)
        {
            List<Reaction> reactionList = new List<Reaction>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCommentsByQuestionID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@questionID", SqlDbType.Int).Value = questionId;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    string volunteerName = dr["VolunteerName"].ToString();
                    string description = dr["Description"].ToString();
                    int volunteerID = Convert.ToInt32(dr["SenderID"].ToString());
                    int reactionID = Convert.ToInt32(dr["ReactionID"].ToString());

                    DateTime timeStamp = Convert.ToDateTime(dr["Timestamp"].ToString());


                    reactionList.Add(new Reaction(reactionID, questionId, volunteerID, description, volunteerName, timeStamp));
                }

                return reactionList;
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
