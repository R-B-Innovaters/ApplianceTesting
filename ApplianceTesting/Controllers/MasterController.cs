using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceTesting.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterControl _i_masterInterface;

        public MasterController(ILogger<HomeController> logger, IMasterControl i_masterInterface)
        {
            _i_masterInterface = i_masterInterface;
        }
        public IActionResult Index()
        {
            ViewBag.userName = HttpContext.Session.GetString("_username");
            TempData["MsgScs"] = null;
            return View();
        }
        #region State Master
        public IActionResult State()
        {
            return View();
        }
        [HttpPost]
        public IActionResult State(StateModel stateModel)
        {
            // var result = _i_masterInterface.AddState(stateModel);
            //if(result.ToString()== "Record Inserted")
            // {
            //     string msg = stateModel.Name+" state has been added successfully!";
            //     TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
            // }
            // return View();
            stateModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            var result = _i_masterInterface.CommonInsertOperation(stateModel, "StateMaster");
            if (result.ToString() == "Record Inserted")
            {
                string msg = stateModel.StateName + " state has been added successfully!";
                TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
            }
            //return View();
            return View(result);
        }
        #endregion

        #region City Master
        public List<Dictionary<string, object>> GetStates()
        {
            return _i_masterInterface.GetRecords("StateMaster", "StateId, StateName");
        }
        public IActionResult City()
        {
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult City(CityModel cityModel)
        {
            cityModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            var result = _i_masterInterface.CommonInsertOperation(cityModel, "CityMaster");
            if (result.ToString() == "Record Inserted")
            {
                string msg = cityModel.CityName + " city has been added successfully!";
                TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
            }
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();
            return View();
        }
        #endregion

        #region Location Master
        public List<Dictionary<string, object>> GetCity()
        {
            return _i_masterInterface.GetRecords("CityMaster", "CityId,CityName");
        }
        public IActionResult GetCitiesByState(int stateId)
        {
            ViewBag.userName = HttpContext.Session.GetString("_username");
            var cities = _i_masterInterface.GetRecords("CityMaster", "CityId, CityName", "", $"StateId = {stateId}");
            var cityList = cities.Select(c => new { Id = c["CityId"], Name = c["CityName"] }).ToList();
            return Json(cityList);
        }

        public IActionResult Location()
        {
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Location(LocationModel locationModel)
        {
            locationModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            var result = _i_masterInterface.CommonInsertOperation(locationModel, "LocationMaster");
            if (result.ToString() == "Record Inserted")
            {
                string msg = locationModel.LocationName + " location has been added successfully!";
                TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
            }
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();
            return View();
        }
        #endregion
        public IActionResult Signout()
        {
            return RedirectToAction("Login", "Home");
        }
        public List<Dictionary<string, object>> GetCityStateLocation()
        {
            return _i_masterInterface.GetRecords("LocationMaster lm", "sm.StateName as State,cm.CityName as City,lm.LocationName as Location", "RIGHT JOIN CityMaster cm ON cm.CityId = lm.CityId RIGHT JOIN StateMaster sm ON sm.StateId = cm.StateId");
        }
        public IActionResult StateCityLocation()
        {
            ViewBag.userName = HttpContext.Session.GetString("_username");
            // var city = GetCity();
            //  ViewBag.City = city.Select(s => new { Id = s["Id"], Name = s["Name"] }).ToList();
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();

            var location = GetCityStateLocation();
            ViewBag.location = location.Select(s => new { State = s["State"], City = s["City"], Location = s["Location"] }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult StateCityLocation(StateModel stateModel, CityModel cityModel, LocationModel locationModel)
        {
            var result = "";
            //Bind Dropdown
            var states = GetStates();
            ViewBag.States = states.Select(s => new { StateId = s["StateId"], StateName = s["StateName"] }).ToList();
            var location = GetCityStateLocation();
            ViewBag.location = location.Select(s => new { State = s["State"], City = s["City"], Location = s["Location"] }).ToList();
            //

            if (stateModel.StateName!=null)
            {
                stateModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                result = _i_masterInterface.CommonInsertOperation(stateModel, "StateMaster");
                if (result.ToString() == "Record Inserted")
                {
                    string msg = stateModel.StateName + " state has been added successfully!";
                    TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
                }
            }
            else if (cityModel.CityName!=null)
            {
                cityModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                result = _i_masterInterface.CommonInsertOperation(cityModel, "CityMaster");
                if (result.ToString() == "Record Inserted")
                {
                    string msg = cityModel.CityName + " city has been added successfully!";
                    TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
                }
            }
            else if (locationModel.LocationName!=null)
            {
                locationModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                result = _i_masterInterface.CommonInsertOperation(locationModel, "LocationMaster");
                if (result.ToString() == "Record Inserted")
                {
                    string msg = locationModel.LocationName + " location has been added successfully!";
                    TempData["MsgScs"] = "showSuccessPopup('" + msg + "');";
                }
            }

            return View();
        }
    }
}
