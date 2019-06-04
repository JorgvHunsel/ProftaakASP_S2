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
            _chat.DeleteChatLog(chatLog);
        }

        public void DeleteMessagesFromDatabase(ChatLog chatLog)
        {
            _chat.DeleteMessages(chatLog);
        }

        public List<ChatMessage> LoadMessageListWithChatId(int chatID) =>
            _chat.LoadMessage(chatID);

        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            _chat.SendMessage(chatid, receiverid, senderid, message);
        }

        public List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid) =>
            _chat.GetAllOpenChatsWithVolunteerId(userid);

        public List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid) => _chat.GetAllOpenChatsWithCareRecipientId(userid);

        public List<ChatLog> GetAllOpenChatsByDate(int userid)
        {
            return _chat.GetAllOpenChatsWithCareRecipientId(userid).OrderByDescending(c => c.TimeStamp).ToList();
        }

        public int CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID) =>
            _chat.CreateNewChatLog(reactionID, volunteerID, careRecipientID);

        public ChatLog GetSingleChatLog(int chatLogId)
        {
            return _chat.GetSingleChatLog(chatLogId);
        }

        public void ChangeChatStatus(ChatLog chatLog)
        {
            _chat.ChangeChatStatus(chatLog);
        }
    }
}
