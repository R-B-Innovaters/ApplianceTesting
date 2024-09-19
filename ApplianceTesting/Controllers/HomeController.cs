using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace ApplianceTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string msg;
        private readonly IHomeControl _i_homeInterfacce;


        public HomeController(ILogger<HomeController> logger, IHomeControl i_homeInterfacce)
        {
            _i_homeInterfacce = i_homeInterfacce;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                var result = _i_homeInterfacce.CheckLogin(login.Username, login.Password);
                if(result.Count>0)
                {
                    //TempData["usrName"]= result.FirstOrDefault().Username;
                    HttpContext.Session.SetString("_username", result.FirstOrDefault().Username);
                   ViewBag.userName = HttpContext.Session.GetString("_username");
                    return RedirectToAction("Index", "Master");
                }
                else {
                    msg = "Username or Password is Incorrect.";
                    TempData["loginErr"] = "showErrorPopup('" + msg + "');";
                }
                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //[HttpPost]
        //public IActionResult Login(LoginModel login)
        //{
        //    if (login.Username == "admin" && login.Password == "admin123")
        //    {
        //        msg = "Correct";
        //        TempData["Msg"] = $"ShowSuccessPopup('{msg}');"; // Use string interpolation and single quotes
        //    }
        //    else
        //    {
        //        msg = "Username or Password is Incorrect.";
        //        TempData["Msg"] = $"ShowErrorPopup('{msg}');"; // Use string interpolation and single quotes
        //    }

        //    return View();


        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
