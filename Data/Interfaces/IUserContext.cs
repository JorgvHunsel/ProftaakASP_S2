using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;

namespace Fontys.PTS2.Prototype.Data.Contexts
{
    public interface IUserContext
    {
        void AddNewUser(User newUser, string password);

        List<User> GetAllUsers();

        int GetUserId(string firstName);

        bool CheckValidityUser(string email, string password);

        User getCurrentUserInfo(string email);

        void EditUser(User currentUser, string password);

        bool CheckIfUserAlreadyExists(string email);

        bool IsEmailValid(string email);

        bool CheckIfAccountIsActive(string email);
    }
}
