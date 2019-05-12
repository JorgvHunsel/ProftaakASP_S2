using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface ILogContext
    {
        void CreateUserLog(Log log);
        List<Log> GetAllLogs();
    }
}
