using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private readonly IUserContext _iUserContext;

        public UserLogic(IUserContext iUserContext)
        {
            _iUserContext = iUserContext;
        }

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public User CheckValidityUser(string email, string password)
        {
            return _iUserContext.CheckValidityUser(email, password);
        }

        public int GetUserId(string firstName)
        {
            return _iUserContext.GetUserId(firstName);
        }

        public List<User> GetAllUsers()
        {
            return _iUserContext.GetAllUsers();
        }

        public void AddNewUser(User newUser, string password)
        {
            _iUserContext.AddNewUser(newUser, password);
        }

        public User getCurrentUserInfo(string email)
        {
            return _iUserContext.getCurrentUserInfo(email);
        }

        public void EditUser(User currentUser, string password)
        {
            _iUserContext.EditUser(currentUser, password);
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return _iUserContext.CheckIfUserAlreadyExists(email);
        }

        public bool IsEmailValid(string email)
        {
            return _iUserContext.IsEmailValid(email);
        }

        public bool CheckIfAccountIsActive(string email)
        {
            return _iUserContext.CheckIfAccountIsActive(email);
        }

    }
}
