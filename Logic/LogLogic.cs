using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class LogLogic
    {
        private readonly ILogContext _logContext;

        public LogLogic(ILogContext logContext)
        {
            _logContext = logContext;
        }

        public void CreateUserLog(int userId, User user)
        {
            _logContext.CreateUserLog(userId, user);
        }
    }
}
