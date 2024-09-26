using ApplianceTesting.DataAccessLayer;
using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceTesting.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterControl _masters;
        private ApplianceTestingDBContext _db;
        public MasterController(ILogger<HomeController> logger, IMasterControl masters,ApplianceTestingDBContext db)
        {
            _masters = masters;
            _db = db;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
           List<NumCounts> nC = _masters.GetCounts();

            return View(nC);
        }

        #region State Master
        public IActionResult State()
        {
            var r= _db.StateMaster.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult State(StateModel stateModel)
        {

            return View();
        }
        #endregion
        #region City Master

        public IActionResult City()
        {
            return View();
        }
        [HttpPost]
        public IActionResult City(CityModel cityModel)
        {

            return View();
        }
        #endregion
        #region Location Master
        public IActionResult Location()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Location(LocationModel locationModel)
        {

            return View();
        }
        #endregion

        public IActionResult Signout()
        {
            return RedirectToAction("Login", "Home");
        }
        public IActionResult GetCitiesByState(int stateId)
        {
            ViewBag.userName = HttpContext.Session.GetString("_username");
            var cities = _masters.GetCityRecords(stateId);
            var cityList = cities.Select(c => new { Id = c.CityId, Name = c.CityName }).ToList();
            return Json(cityList);

        }
        public IActionResult StateCityLocation()
        {
            // var r = _db.StateMaster.Select(s => new { s.StateId, s.StateName }).ToList();

            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
            ViewBag.States = _masters.GetStateRecords();
            ViewBag.Locations= _masters.GetStateCityLocRecords();
            return View();
        }

        [HttpPost]
        public IActionResult StateCityLocation(StateModel stateModel, CityModel cityModel, LocationModel locationModel)
        {
            string msg = "";
            ViewBag.States = _masters.GetStateRecords();
            if (stateModel.StateName != null)
            {
                bool result = _masters.InsertModel(stateModel);
                if (result == true)
                {
                    msg = "Record Inserted";
                    TempData["MsgScs"] = $"showSuccessPopup('{msg}');";
                }
                else
                {
                    msg = "Oops! Error while inserting...";
                    TempData["MsgScs"] = $"showErrorPopup('{msg}');";
                }
            }
            else if (cityModel.CityName != null)
            {
                bool result = _masters.InsertModel(cityModel);
                if (result == true)
                {
                    msg = "Record Inserted";
                    TempData["MsgScs"] = $"showSuccessPopup('{msg}');";
                }
                else
                {
                    msg = "Oops! Error while inserting...";
                    TempData["MsgScs"] = $"showErrorPopup('{msg}');";
                }
            }
            else if (locationModel.LocationName !=null)
            {
                bool result = _masters.InsertModel(locationModel);
                if (result == true)
                {
                    msg = "Record Inserted";
                    TempData["MsgScs"] = $"showSuccessPopup('{msg}');";
                }
                else
                {
                    msg = "Oops! Error while inserting...";
                    TempData["MsgScs"] = $"showErrorPopup('{msg}');";
                }
            }
            string str = "";
            return View();
        }
    }
}
