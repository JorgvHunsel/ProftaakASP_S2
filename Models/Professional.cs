using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Professional : User
    {
        public Professional(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateTime, Gender userGender, bool status, AccountType accountType, string password) : base(firstName, lastName, address, city, postalCode, emailAddress, dateTime, userGender, status, accountType, password)
        {
        }

        public Professional(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateBirth, Gender userGender, bool status, AccountType accountType, string password) : base(userId, firstName, lastName, address, city, postalCode, emailAddress, dateBirth, userGender, status, accountType, password)
        {
        }

    }
}
