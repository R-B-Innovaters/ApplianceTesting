using ApplianceTesting.DataAccessLayer;
using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ApplianceTesting.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterControl _masters;
        private ApplianceTestingDBContext _db;
        public MasterController(ILogger<HomeController> logger, IMasterControl masters, ApplianceTestingDBContext db)
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

        //#region State Master
        //public IActionResult State()
        //{
        //    var r= _db.StateMaster.ToList();
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult State(StateModel stateModel)
        //{

        //    return View();
        //}
        //#endregion
        //#region City Master

        //public IActionResult City()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult City(CityModel cityModel)
        //{

        //    return View();
        //}
        //#endregion
        //#region Location Master
        //public IActionResult Location()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Location(LocationModel locationModel)
        //{

        //    return View();
        //}
        //#endregion

        public IActionResult Signout()
        {
            return RedirectToAction("Login", "Home");
        }
        public IActionResult GetCitiesByState(int stateId)
        {
            var cities = _masters.GetCityRecords(stateId);
            var cityList = cities.Select(c => new { Id = c.CityId, Name = c.CityName }).ToList();
            return Json(cityList);

        }
        public IActionResult GetLocationsByCity(int cityId)
        {
            var locations = _masters.GetLocationRecords(cityId);
            var locationList = locations.Select(c => new { Id = c.LocationId, Name = c.LocationName }).ToList();
            return Json(locationList);

        }
        public IActionResult StateCityLocation()
        {
            // var r = _db.StateMaster.Select(s => new { s.StateId, s.StateName }).ToList();

            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
            ViewBag.States = _masters.GetStateRecords();
            ViewBag.Cities = _masters.GetCityRecords(null);
            ViewBag.Locations = _masters.GetStateCityLocRecords();
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
            else if (locationModel.LocationName != null)
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

        [HttpPost]
        public IActionResult UpdateStatus(string tableName, string idColumn, int id,string stColumn ,bool status )
        {
            //var location = _db.LocationMaster.Find(id);
            //if (location == null)
            //{
            //    return NotFound();
            //}

            //location.LocationStatus = status;
            //_db.SaveChanges();

            //return Ok(new { success = true });

            if (string.IsNullOrEmpty(tableName) && string.IsNullOrEmpty(idColumn))
            {
                return BadRequest("Table name cannot be null or empty.");
            }

            var success = _masters.UpdateStatusModel(tableName, idColumn, id, stColumn, status);
            if (!success)
            {
                return NotFound();
            }

            return Ok(new { success = true });
        }

        public IActionResult AddCompany()
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }

            var states = _masters.GetStateRecords(); 
            var filteredStates = states.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName
            }).ToList();

            ViewBag.stateList = filteredStates; 
            ViewBag.compList = _masters.GetCompanies();
            return View();
        }
        [HttpPost]
        public IActionResult AddCompany(CompanyModel compModel)
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
            ViewBag.stateList = _masters.GetStateRecords();
            if (compModel != null)
            {
                string msg = "";
                compModel.CreatedDate = DateTime.Now;
                //string inputDate = Convert.ToDateTime(compModel.CompRegistrationDate).ToString("yyyy-MM-dd");
                //compModel.CompRegistrationDate = Convert.ToDateTime(dt);
               

                bool result = _masters.InsertModel(compModel);
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

            return View("AddCompany");
        }
    }
}

