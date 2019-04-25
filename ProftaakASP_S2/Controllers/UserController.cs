using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;

        public UserController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel userViewModel)
        {
            try
            {
                User newCustomer = _userLogic.CheckValidityUser(userViewModel.EmailAddress, userViewModel.Password);

                HttpContext.Response.Cookies.Append("id", newCustomer.UserId.ToString());
                HttpContext.Response.Cookies.Append("name", newCustomer.FirstName);
                HttpContext.Response.Cookies.Append("role", newCustomer.UserAccountType.ToString());

                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Gegevens komen niet overeen";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("id");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("role");
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel, string password, string passwordValidation)
        {
            if (password == passwordValidation)
            {
                if (userViewModel.UserAccountType == global::Models.User.AccountType.CareRecipient)
                {
                    _userLogic.AddNewUser(new CareRecipient(userViewModel.FirstName, userViewModel.LastName, userViewModel.Address, userViewModel.City, userViewModel.PostalCode, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), userViewModel.UserGender, true, global::Models.User.AccountType.CareRecipient), password);
                }
                else
                {
                    _userLogic.AddNewUser(new Volunteer(userViewModel.FirstName, userViewModel.LastName, userViewModel.Address, userViewModel.City, userViewModel.PostalCode, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), userViewModel.UserGender, true, global::Models.User.AccountType.Volunteer), password);
                }
            }
            else
            {
                ViewBag.Message = "De wachtwoorden komen niet overheen";
                return View();
            }



            return View();
        }
    }
}