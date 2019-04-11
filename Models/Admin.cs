using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fontys.PTS2.Prototype.Classes
{
    class Admin : User
    {
        public Admin(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateBirth, Gender userGender, bool status, AccountType accountType) : base(firstName, lastName, address, city, postalCode, emailAddress, dateBirth, userGender, status, accountType)
        {
        }

        public Admin(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateBirth, Gender userGender, bool status, AccountType accountType) : base(userId, firstName, lastName, address, city, postalCode, emailAddress, dateBirth, userGender, status, accountType)
        {
        }

    }
}
