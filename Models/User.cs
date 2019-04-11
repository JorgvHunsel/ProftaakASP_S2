using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fontys.PTS2.Prototype.Classes
{
    public abstract class User
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { M, V, A }
        public int UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string EmailAddress { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender UserGender { get;  private set; }
        public AccountType UserAccountType { get; private set; }
        public bool Status { get; set; }

        protected User(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
        }

        protected User(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
        }

        
    }
}
