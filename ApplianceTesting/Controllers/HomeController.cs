using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace ApplianceTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeControl _i_homeInterfacce;
        
        string msg;

        public HomeController(ILogger<HomeController> logger, IHomeControl i_homeInterfacce)
        {
            _i_homeInterfacce = i_homeInterfacce;
        
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel login)
        {
            try
            {
                var result = _i_homeInterfacce.GetUser(login.Username,login.Password);
                if (result!=null)
                {
                  HttpContext.Session.SetString("_username", result.Username);
                    HttpContext.Session.SetString("_userid", result.UserId.ToString());
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
