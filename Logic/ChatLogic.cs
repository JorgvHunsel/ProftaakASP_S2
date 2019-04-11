using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Models;

namespace Logic
{
    class ChatLogic
    {
        ChatContextSQL chatRepo = new ChatContextSQL();

        public DataTable GetAllOpenChatsAsDataTable(int userid)
        {
            return GetAllOpenChatsAsDataTable(userid);
        }

        public List<ChatMessage> LoadMessageListWithChatID(int chatID) =>
            chatRepo.LoadMessageAsListUsingChatLogID(chatID);

        //Wesley
        public static List<ChatLog> LoadOpenMessageList()
        {
            ChatContextSQL chatRepo = new ChatContextSQL();
            return chatRepo.LoadOpenChatsList();
        }
        //End Wesley
        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            chatRepo.SendMessage(chatid, receiverid, senderid, message);
        }

        public List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid) =>
            chatRepo.GetAllOpenChatsWithVolunteerID(userid);
        public List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid) => chatRepo.GetAllOpenChatsWithCareRecipientID(userid);

        public void CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID) =>
            chatRepo.CreateNewChatLog(reactionID, volunteerID, careRecipientID);
    }
}
