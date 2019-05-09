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
    public class ChatLogic
    {
        private readonly IChatContext _chat;

        public ChatLogic(IChatContext chat)
        {
            _chat = chat;
        }

        public List<ChatLog> GetAllChatLogs()
        {
            return _chat.GetAllChatLogs();
        }

        public void DeleteChatLogFromDatabase(ChatLog chatLog)
        {
            _chat.DeleteChatLogFromDatabase(chatLog);
        }

        public void DeleteMessagesFromDatabase(ChatLog chatLog)
        {
            _chat.DeleteMessagesFromDatabase(chatLog);
        }

        //TODO: Check if method is still necessary
        public DataTable GetAllOpenChatsAsDataTable(int userid)
        {
            return GetAllOpenChatsAsDataTable(userid);
        }

        public List<ChatMessage> LoadMessageListWithChatID(int chatID) =>
            _chat.LoadMessageAsListUsingChatLogID(chatID);

        //TODO: Check if method is still necessary
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

        public List<ChatLog> GetAllOpenChatsByDate(int userid)
        {
            return _chat.GetAllOpenChatsWithCareRecipientID(userid).OrderByDescending(c => c.TimeStamp).ToList();
        }

        public int CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID) =>
            _chat.CreateNewChatLog(reactionID, volunteerID, careRecipientID); 
    }
}
