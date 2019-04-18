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
        private readonly IUserContext _user;

        public UserLogic(IUserContext user)
        {
            _user = user;
        }

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public User CheckValidityUser(string email, string password)
        {
            return _user.CheckValidityUser(email, password);
        }

        public int GetUserId(string firstName)
        {
            return _user.GetUserId(firstName);
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAllUsers();
        }

        public void AddNewUser(User newUser, string password)
        {
            _user.AddNewUser(newUser, password);
        }

        public User getCurrentUserInfo(string email)
        {
            return _user.getCurrentUserInfo(email);
        }

        public void EditUser(User currentUser, string password)
        {
            _user.EditUser(currentUser, password);
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return _user.CheckIfUserAlreadyExists(email);
        }

        public bool IsEmailValid(string email)
        {
            return _user.IsEmailValid(email);
        }

        public bool CheckIfAccountIsActive(string email)
        {
            return _user.CheckIfAccountIsActive(email);
        }

        public User GetUserById(int userId)
        {
            return _user.GetUserById(userId);
        }


    }
}
