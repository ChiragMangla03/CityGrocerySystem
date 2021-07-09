using CGS.DAL;
using CGS.Model;
using CityGrocerySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityGrocerySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MasterDataDetails MdataDetail = new MasterDataDetails();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDetails login)
        {  
            int user_type = MdataDetail.CheckValidUser(login);
            if (user_type == 1)
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user_type == 2)
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegistrationDetails userdetail)
        {
            TryValidateModel(userdetail);
            if (ModelState.IsValid)
            {
                string a = MdataDetail.RegisterNewUser(userdetail);
                return RedirectToAction("Index");
            }
            else
            {
                return View(userdetail);
            }

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
