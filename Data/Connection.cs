using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    class Connection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
