using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;

namespace Fontys.PTS2.Prototype.Data.Contexts
{
    public interface IChatContext
    {
        List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid);
        List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid);
        List<ChatMessage> LoadMessageAsListUsingChatLogID(int chatID);

        //Wesley
        List<ChatLog> LoadOpenChatsList();

        //End Wesley
        void SendMessage(int chatid, int receiverid, int senderid, string message);
        void CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID);
    }
}
