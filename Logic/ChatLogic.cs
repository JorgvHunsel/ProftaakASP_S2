using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    class ChatLogic
    {
        private readonly IChatContext _chat;

        public ChatLogic(IChatContext chat)
        {
            _chat = chat;
        }

        public DataTable GetAllOpenChatsAsDataTable(int userid)
        {
            return GetAllOpenChatsAsDataTable(userid);
        }

        public List<ChatMessage> LoadMessageListWithChatID(int chatID) =>
            _chat.LoadMessageAsListUsingChatLogID(chatID);

        //Wesley
        public List<ChatLog> LoadOpenMessageList()
        {
            return _chat.LoadOpenChatsList();
        }
        //End Wesley
        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            _chat.SendMessage(chatid, receiverid, senderid, message);
        }

        public List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid) =>
            _chat.GetAllOpenChatsWithVolunteerID(userid);
        public List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid) => _chat.GetAllOpenChatsWithCareRecipientID(userid);

        public void CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID) =>
            _chat.CreateNewChatLog(reactionID, volunteerID, careRecipientID);
    }
}
