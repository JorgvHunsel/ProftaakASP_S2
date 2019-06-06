using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Data.Contexts;
using Logic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class LogTests
    {
        private LogLogic _logLogic;
        private LogContextMock _logContext;

        [TestInitialize]
        public void TestInitializer()
        {
            _logContext = new LogContextMock();
            _logLogic = new LogLogic(_logContext);
        }

        [TestMethod]
        public void CreateUserLog()
        {
            Assert.AreEqual(0,_logContext.logList.Count);

            User _user = new Volunteer(14, "Wesley", "Martens", "Drenthelaan 1", "Drenthe", "2101SZ", "wesley@hotmail.com", DateTime.Now, User.Gender.Man, true, User.AccountType.Professional,"1111");
            _logLogic.CreateUserLog(_user.UserId, _user);
            Assert.AreEqual(14,_logContext.logList[0].UserId);

            User _user2 = new Volunteer(15, "Boaz", "Martens", "Drenthelaan 1", "Drenthe", "2101SZ", "boaz@hotmail.com", DateTime.Now, User.Gender.Man, false, User.AccountType.Professional,"1111");
            _logLogic.CreateUserLog(_user2.UserId, _user2);
            Assert.AreEqual(15,_logContext.logList[1].UserId);
        }

        [TestMethod]
        public void GetAllLogs()
        {
            Assert.AreEqual(0, _logLogic.GetAllLogs().Count);

            _logContext.logList.Add(new Log(1, 1, "title", "description", DateTime.Now));
            Assert.AreEqual( 1,_logLogic.GetAllLogs().Count);

            _logContext.logList.Add(new Log(2, 1, "title", "description", DateTime.Now));
            Assert.AreEqual(2,_logLogic.GetAllLogs().Count);
        }
    }
}
