using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
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
        int CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID);
        void DeleteChatLogFromDatabase(ChatLog chatLog);
        void DeleteMessagesFromDatabase(ChatLog chatLog);
        List<ChatLog> GetAllChatLogs();
        ChatLog GetSingleChatLog(int chatLogId);
        void ChangeChatStatus(ChatLog chatLog);
    }
}
