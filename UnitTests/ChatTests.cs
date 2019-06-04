using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Logic;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void GetAllOpenChatsWithVolunteerID_IsValid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IChatContext>()
                    .Setup(x => x.GetAllOpenChatsWithVolunteerId(1))
                    .Returns(GetSampleChats());

                var cls = mock.Create<ChatLogic>();
                var expected = GetSampleChats();

                var actual = cls.GetAllOpenChatsWithVolunteerID(1);

                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetAllOpenChatsWithCareRecipientID_IsValid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IChatContext>()
                    .Setup(x => x.GetAllOpenChatsWithCareRecipientId(1))
                    .Returns(GetSampleChats());

                var cls = mock.Create<ChatLogic>();
                var expected = GetSampleChats();

                var actual = cls.GetAllOpenChatsWithCareRecipientID(1);

                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [TestMethod]
        public void LoadMessageAsListUsingChatLogID_IsValid()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();
            List<ChatMessage> stub = new List<ChatMessage>();

            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM", "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,"1111").Object;
            mockContext.Setup(x => x.LoadMessage(user.UserId))
                .Returns(stub);

            var _chatLogic = new ChatLogic(mockContext.Object);

            var result = _chatLogic.LoadMessageListWithChatId(user.UserId);
            var expected = stub;
            

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ChatMessage>));
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SendMessage_Called_Exactly_Once()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();
            mockContext.Setup(x => x.SendMessage(0, 1, 2, "hoi"));

            var _chatLogic = new ChatLogic(mockContext.Object);
            _chatLogic.SendMessage(0, 1, 2, "hoi");
            mockContext.Verify((x => x.SendMessage(0,1,2,"hoi")), Times.Exactly(1));
        }

        [TestMethod]
        public void CreateNewChatLog_IsValid()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();

            int newChatLogId = 3;
            mockContext.Setup(x => x.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(newChatLogId);

            var _chatLogic = new ChatLogic(mockContext.Object);
            var result = _chatLogic.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            mockContext.Verify(x => x.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(newChatLogId, result);
            

        }

        private List<ChatLog> GetSampleChats()
        {
            List<ChatLog> output = new List<ChatLog>
            {
                new ChatLog(12, "foo", 12, 13, "baa", "doo", "aah", "hi", DateTime.Today, 18, true)
            };
            return output;
        }
    }
}
