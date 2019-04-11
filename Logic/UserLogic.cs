using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Logic
{
    public class UserLogic
    {
        static UserRepository userRepo = new UserRepository();

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public static bool CheckValidityUser(string email, string password)
        {
            return userRepo.CheckValidityUser(email, password);
        }

        public static int GetUserId(string firstName)
        {
            return userRepo.GetUserId(firstName);
        }

        public static List<User> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public static void AddNewUser(User newUser, string password)
        {
            userRepo.AddNewUser(newUser, password);
        }

        public static User getCurrentUserInfo(string email)
        {
            return userRepo.getCurrentUserInfo(email);
        }

        public static void EditUser(User currentUser, string password)
        {
            userRepo.EditUser(currentUser, password);
        }

        public static bool CheckIfUserAlreadyExists(string email)
        {
            return userRepo.CheckIfUserAlreadyExists(email);
        }

        public static bool IsEmailValid(string email)
        {
            return userRepo.IsEmailValid(email);
        }

        public static bool CheckIfAccountIsActive(string email)
        {
            return userRepo.CheckIfAccountIsActive(email);
        }

    }
}
