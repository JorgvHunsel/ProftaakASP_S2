using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void AddNewUser(User newUser, string password);
        List<User> GetAllUsers();
        int GetUserId(string firstName);
        User CheckValidityUser(string email, string password);
        User GetCurrentUserInfo(string email);
        void EditUser(User currentUser, string password);
        bool CheckIfUserAlreadyExists(string email);
        bool IsEmailValid(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserById(int userId);
    }
}
