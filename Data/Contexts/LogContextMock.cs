using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class LogContextMock : ILogContext
    {

        public List<Log> logList = new List<Log>();

        public void CreateUserLog(Log log)
        {
            logList.Add(log);
        }

        public List<Log> GetAllLogs()
        {
            return logList;
        }
    }
}
