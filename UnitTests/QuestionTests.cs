using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        public void TestMethod2()
        {

        }

        [TestMethod]
        public void TestMethod3()
        {

        }

        [TestMethod]
        public void TestMethod4()
        {

        }

        [TestMethod]
        public void TestMethod5()
        {

        }
    }
}
