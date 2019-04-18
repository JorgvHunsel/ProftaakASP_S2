using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserLogic _userLogic;

        public LoginController(UserLogic userLogic)
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

                ViewBag.Message = "Ingelogd!";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Gegevens komen niet overeen";
                return View();
            }
        }


    }
}