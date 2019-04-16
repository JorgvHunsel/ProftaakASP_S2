using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ProftaakASP_S2.Models
{
    public class LoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginViewModel(User user, string password)
        {
            EmailAddress = user.EmailAddress;
            Password = password;
        }

        public LoginViewModel()
        {
            
        }
    }
}

