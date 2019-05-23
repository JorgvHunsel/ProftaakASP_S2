using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }
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
        public string Password { get; set; }

        protected User(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
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
            Password = password;
        }

        protected User(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
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
            Password = password;
        }
        
    }
}
