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
    public class UserTests
    {
        User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM", "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,"1111").Object;
        Mock<IUserContext> mockContext = new Mock<IUserContext>();

        [TestMethod]
        public void AddNewUser_IsValid()
        {
            mockContext.Setup(x => x.AddNewUser(user));

            var _userLogic = new UserLogic(mockContext.Object);
            _userLogic.AddNewUser(user);

            mockContext.Verify(x => x.AddNewUser(user), Times.Exactly(1));
        }

        [TestMethod]
        public void EditUser_IsValid()
        {
            mockContext.Setup(x => x.EditUser(user, "secret"));

            var _userLogic = new UserLogic(mockContext.Object);
            _userLogic.EditUser(user, "secret");

            mockContext.Verify(x => x.EditUser(user, "secret"), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllUsers_IsValid()
        {
            List<User> testList = new List<User>();

            mockContext.Setup(x => x.GetAllUsers())
                .Returns(testList);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.GetAllUsers();

            mockContext.Verify(x => x.GetAllUsers(), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(List<User>));
        }

        [TestMethod]
        public void GetUserId_IsValid()
        {
            mockContext.Setup(x => x.GetUserId(user.EmailAddress))
                .Returns(It.IsAny<int>);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.GetUserId(user.EmailAddress);

            mockContext.Verify(x => x.GetUserId(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_IsValid()
        {
            mockContext.Setup(x => x.CheckIfAccountIsActive(user.EmailAddress))
                .Returns(It.IsAny<bool>);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.CheckIfAccountIsActive(user.EmailAddress);

            mockContext.Verify(x => x.CheckIfAccountIsActive(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckValidityUser_IsValid()
        {
            mockContext.Setup(x => x.CheckValidityUser(user.EmailAddress, "secret"))
                .Returns(user);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.CheckValidityUser(user.EmailAddress, "secret");

            mockContext.Verify(x => x.CheckValidityUser(user.EmailAddress, "secret"), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_IsValid()
        {
            mockContext.Setup(x => x.CheckIfUserAlreadyExists(user.EmailAddress))
                .Returns(It.IsAny<bool>);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.CheckIfUserAlreadyExists(user.EmailAddress);

            mockContext.Verify(x => x.CheckIfUserAlreadyExists(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void GetCurrentUserInfo_IsValid()
        {
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM", "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,"1111").Object;
            
            mockContext.Setup(x => x.GetCurrentUserInfo(user.EmailAddress))
                .Returns(user);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.GetCurrentUserInfo(user.EmailAddress);

            mockContext.Verify(x => x.GetCurrentUserInfo(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void GetUserById_IsValid()
        {
            mockContext.Setup(x => x.GetUserById(user.UserId))
                .Returns(user);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.GetUserById(user.UserId);

            mockContext.Verify(x => x.GetUserById(user.UserId), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void IsEmailValid_IsValid()
        {
            mockContext.Setup(x => x.IsEmailValid(user.EmailAddress))
                .Returns(It.IsAny<bool>);

            var _userLogic = new UserLogic(mockContext.Object);
            var result = _userLogic.IsEmailValid(user.EmailAddress);

            mockContext.Verify(x => x.IsEmailValid(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }
    }
}
