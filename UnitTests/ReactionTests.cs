using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using Models;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ReactionTests
    {
        [TestMethod]
        public void PostReaction_IsValid()
        {
            Mock<IReactionContext> mockContext = new Mock<IReactionContext>();
            Mock<Reaction> mockReaction = new Mock<Reaction>(1, 2, "foo");
            
            mockContext.Setup(x => x.PostReaction(mockReaction.Object));

            var _reactionLogic = new ReactionLogic(mockContext.Object);
            _reactionLogic.PostReaction(mockReaction.Object);

            mockContext.Verify(x => x.PostReaction(mockReaction.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllCommentsWithQuestionID()
        {
            Mock<IReactionContext> mockContext = new Mock<IReactionContext>();
            Mock<Category> _category = new Mock<Category>("Medisch");
            Mock<Question> _question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today, true, _category.Object, 12);
            List<Reaction> _mockList = new List<Reaction>();
            mockContext.Setup(x => x.GetAllCommentsWithQuestionID(_question.Object.QuestionId))
                .Returns(_mockList);

            var _reactionLogic = new ReactionLogic(mockContext.Object);
            var result = _reactionLogic.GetAllCommentsWithQuestionID(_question.Object.QuestionId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Reaction>));
        }
    }
}
