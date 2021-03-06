﻿using System;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly LogLogic _logLogic;

        public UserController(UserLogic userLogic, LogLogic logLogic)
        {
            _userLogic = userLogic;
            _logLogic = logLogic;
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

                if (!newCustomer.Status)
                {
                    ViewBag.Message = "Dit account is geblokkeerd. Neem contact op met de administrator.";
                    return View();
                }

                HttpContext.Response.Cookies.Append("id", newCustomer.UserId.ToString());
                HttpContext.Response.Cookies.Append("name", newCustomer.FirstName);
                HttpContext.Response.Cookies.Append("role", newCustomer.UserAccountType.ToString());
                HttpContext.Response.Cookies.Append("email", newCustomer.EmailAddress);

                switch (newCustomer.UserAccountType)
                {
                    case global::Models.User.AccountType.Admin:
                        return RedirectToAction("QuestionOverview", "Admin");
                    case global::Models.User.AccountType.Volunteer:
                        return RedirectToAction("QuestionOverview", "Volunteer");
                    case global::Models.User.AccountType.Professional:
                        return RedirectToAction("QuestionOverview", "Professional");
                    default:
                        return RedirectToAction("Overview", "CareRecipient");
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "De gegevens zijn niet ingevuld";
                return View();
            }
            catch(IndexOutOfRangeException)
            {
                ViewBag.Message = "De gegevens komen niet overeen";
                return View();
            }

        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("id");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("email");
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
            try
            {
                if (password == passwordValidation)
                {
                    switch (userViewModel.UserAccountType)
                    {
                        case global::Models.User.AccountType.CareRecipient:
                            _userLogic.AddNewUser(new CareRecipient(userViewModel.FirstName, userViewModel.LastName,
                                userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                                userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                                (User.Gender) Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                global::Models.User.AccountType.CareRecipient, password));
                            break;
                        case global::Models.User.AccountType.Admin:
                            _userLogic.AddNewUser(new Admin(userViewModel.FirstName, userViewModel.LastName,
                                userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                                userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                                (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                global::Models.User.AccountType.Admin, password));
                            break;
                        default:
                            _userLogic.AddNewUser(
                                new Volunteer(userViewModel.FirstName, userViewModel.LastName, userViewModel.Address,
                                    userViewModel.City, userViewModel.PostalCode, userViewModel.EmailAddress,
                                    Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                    global::Models.User.AccountType.Volunteer, password));
                            break;
                    }
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }


            }
            catch (FormatException)
            {
                ViewBag.Message = "De geboortedatum is onjuist ingevoerd";
                return View();
            }


            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult CreateAccountProfessional()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccountProfessional(UserViewModel userViewModel, string password, string passwordValidation)
        {
            try
            {
                if (password == passwordValidation)
                {
                    _userLogic.AddNewUser(new CareRecipient(userViewModel.FirstName, userViewModel.LastName,
                        userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                        userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                        (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                        global::Models.User.AccountType.Professional, password));
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }
            }
            catch (FormatException)
            {
                ViewBag.Message = "De geboortedatum is onjuist ingevoerd";
                return View();
            }


            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult AccountOverview()
        {
            User currentUser = _userLogic.GetCurrentUserInfo(Request.Cookies["email"]);
            return View("AccountOverview", new UserViewModel(currentUser));
        }

        [HttpGet]
        public ActionResult EditAccount()
        {
            User user = _userLogic.GetUserById(Convert.ToInt32(Request.Cookies["id"]));

            return View("EditAccount", new UserViewModel(user));
        }


        [HttpPost]
        public ActionResult EditAccount(UserViewModel userView)
        {
            switch (userView.UserAccountType)
            {
                case global::Models.User.AccountType.CareRecipient:
                    _userLogic.EditUser(new CareRecipient(userView.UserId ,userView.FirstName, userView.LastName,
                        userView.Address, userView.City, userView.PostalCode,
                        userView.EmailAddress, Convert.ToDateTime(userView.BirthDate),
                        (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                        global::Models.User.AccountType.CareRecipient, ""), "");
                    break;
                case global::Models.User.AccountType.Admin:
                    _userLogic.EditUser(new Admin(userView.UserId, userView.FirstName, userView.LastName,
                        userView.Address, userView.City, userView.PostalCode,
                        userView.EmailAddress, Convert.ToDateTime(userView.BirthDate),
                        (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                        global::Models.User.AccountType.Admin, ""), "");
                    break;
                default:
                    _userLogic.EditUser(new Volunteer(userView.UserId, userView.FirstName, userView.LastName, userView.Address,
                        userView.City, userView.PostalCode, userView.EmailAddress,
                        Convert.ToDateTime(userView.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                        global::Models.User.AccountType.Volunteer, "") ,"");
                    break;
            }


            return RedirectToAction("AccountOverview");
        }
        public ActionResult BlockUser(int userId)
        {
            User updatedUser = _userLogic.GetUserById(userId);

            updatedUser.Status = !updatedUser.Status;

            _userLogic.EditUser(updatedUser, "");

            _logLogic.CreateUserLog(Convert.ToInt32(Request.Cookies["id"]), updatedUser);

            return RedirectToAction("Logout");
        }
    }
}