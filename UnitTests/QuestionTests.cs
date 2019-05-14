﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Data.Interfaces;
using Models;
using System;
using Logic;

namespace UnitTests
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void WriteQuestionToDatabase_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> _category = new Mock<Category>("Medisch");
            Mock<Question> _question = new Mock<Question>(1,"foo","baa", Question.QuestionStatus.Open, DateTime.Today, true, _category.Object, 12);
            mockContext.Setup(x => x.WriteQuestionToDatabase(_question.Object));

            var _questionLogic = new QuestionLogic(mockContext.Object);
            _questionLogic.WriteQuestionToDatabase(_question.Object);
            mockContext.Verify(x => x.WriteQuestionToDatabase(_question.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllOpenQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            mockContext.Setup(x => x.GetAllOpenQuestions())
                .Returns(stub);

            var _questionLogic = new QuestionLogic(mockContext.Object);
            var result = _questionLogic.GetAllOpenQuestions();

            Assert.IsInstanceOfType(result, typeof(List<Question>));

        }

        [TestMethod]
        public void GetAllOpenQuestionsCareRecipientID_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM", "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient).Object;
            mockContext.Setup(x => x.GetAllOpenQuestionsCareRecipientID(user.UserId))
                .Returns(stub);

            var _questionLogic = new QuestionLogic(mockContext.Object);
            var result = _questionLogic.GetAllOpenQuestionCareRecipientID(user.UserId);

            Assert.IsInstanceOfType(result, typeof(List<Question>));
        }

        [TestMethod]
        public void GetAllClosedQuestionsCareRecipientID_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM", "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient).Object;
            mockContext.Setup(x => x.GetAllClosedQuestionsCareRecipientID(user.UserId))
                .Returns(stub);

            var _questionLogic = new QuestionLogic(mockContext.Object);
            var result = _questionLogic.GetAllClosedQuestionsCareRecipientId(user.UserId);

            Assert.IsInstanceOfType(result, typeof(List<Question>));
        }

        [TestMethod]
        public void GetSingleQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> _category = new Mock<Category>("Medisch");
            Mock<Question> _question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today, true, _category.Object, 12);
            mockContext.Setup(x => x.GetSingleQuestion(_question.Object.QuestionId))
                .Returns(_question.Object);

            var _questionLogic = new QuestionLogic(mockContext.Object);
            var result = _questionLogic.GetSingleQuestion(_question.Object.QuestionId);
            
            Assert.IsInstanceOfType(result, typeof(Question));
        }

        [TestMethod]
        public void EditQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> _category = new Mock<Category>("Medisch");
            Mock<Question> _question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today, true, _category.Object, 12);
            mockContext.Setup(x => x.EditQuestion(_question.Object));

            var _questionLogic = new QuestionLogic(mockContext.Object);
            _questionLogic.EditQuestion(_question.Object);

            mockContext.Verify(x => x.EditQuestion(_question.Object), Times.Exactly(1));

        }
    }
}
