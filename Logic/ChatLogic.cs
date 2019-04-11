using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Logic
{
    class ChatLogic
    {
        ChatRepository chatRepo = new ChatRepository();

        public DataTable GetAllOpenChatsAsDataTable(int userid)
        {
            return GetAllOpenChatsAsDataTable(userid);
        }

        public List<ChatMessage> LoadMessageListWithChatID(int chatID) => chatRepo.LoadMessageListWithChatID(chatID);

        //Wesley
        public static List<ChatLog> LoadOpenMessageList()
        {
            ChatRepository chatRepo = new ChatRepository();
            return chatRepo.LoadOpenChatsList();
        }
        //End Wesley
        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            chatRepo.SendMessage(chatid, receiverid, senderid, message);
        }

        public List<ChatLog> GetAllOpenChatsWithVolunteerID(int userid) => chatRepo.GetAllChatsWithVolunteerID(userid);
        public List<ChatLog> GetAllOpenChatsWithCareRecipientID(int userid) => chatRepo.GetAllOpenChatsWithCareRecipientID(userid);

        public void CreateNewChatLog(int reactionID, int volunteerID, int careRecipientID) =>
            chatRepo.CreateNewChatLog(reactionID, volunteerID, careRecipientID);
    }
}
