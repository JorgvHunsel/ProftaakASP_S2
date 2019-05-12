using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Log
    {
        public int LogId { get; private set; }
        public int UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateTime { get; private set; }

        public Log(int logId, int userId, string title, string description, DateTime dateTime)
        {
            LogId = logId;
            UserId = userId;
            Title = title;
            Description = description;
            DateTime = dateTime;
        }

        public Log(int userId, string title, string description, DateTime dateTime)
        {
            UserId = userId;
            Title = title;
            Description = description;
            DateTime = dateTime;
        }
    }
}
