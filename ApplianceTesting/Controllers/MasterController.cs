using ApplianceTesting.DataAccessLayer;
using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

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
            var cities = _masters.GetCityRecords(stateId).Where(s => s.CityStatus == true);
            var cityList = cities.Select(c => new { Id = c.CityId, Name = c.CityName }).ToList();
            return Json(cityList);

        }
        public IActionResult GetLocationsByCity(int cityId)
        {
            var locations = _masters.GetLocationRecords(cityId).Where(s => s.LocationStatus == true);
            var locationList = locations.Select(c => new { Id = c.LocationId, Name = c.LocationName }).ToList();
            return Json(locationList);

        }
        public IActionResult AddRole()
        {
            // var r = _db.StateMaster.Select(s => new { s.StateId, s.StateName }).ToList();

            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
                ViewBag.Id = HttpContext.Session.GetString("_userid").ToString();
            }

            ViewBag.roleList = _masters.GetRole();
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(RoleModel roleModel)
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Id = HttpContext.Session.GetString("_userid").ToString();
            }
            string msg = "";
            if (roleModel.RoleName != null)
            {
                roleModel.CreatedDate = DateTime.Now;
                roleModel.CreatedBy = Convert.ToInt32(ViewBag.Id);
                bool result = _masters.InsertModel(roleModel);
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
            ViewBag.roleList = _masters.GetRole();
            return View("AddRole");
        }

        public IActionResult ManageCredential()
        {
            // var r = _db.StateMaster.Select(s => new { s.StateId, s.StateName }).ToList();

            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
                ViewBag.Id = HttpContext.Session.GetString("_userid").ToString();
            }
            //dropdown bind
            var roleList = _masters.GetRole();
            var filteredroleList = roleList.Select(s => new RoleModel
            {
                RoleId = s.RoleId,
                RoleName = s.RoleName,
            }).ToList();
            ViewBag.roleDDL = filteredroleList;

            var compList = _masters.GetCompanies();
            var filteredCompanies = compList.Select(s => new CompanyModel
            {
                CompanyId = s.CompanyId,
                CompanyName = s.CompanyName,
            }).ToList();
            ViewBag.compDDL = filteredCompanies;
            //
            ViewBag.UserList = _masters.GetUsersList();
            return View();
        }

        [HttpPost]
        public IActionResult CredentialManage(UserModel userModel)
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Id = HttpContext.Session.GetString("_userid").ToString();
            }
            //dropdown bind
            var roleList = _masters.GetRole();

            var filteredroleList = roleList.Select(s => new RoleModel
            {
                RoleId = s.RoleId,
                RoleName = s.RoleName,
            }).ToList();
            ViewBag.roleDDL = filteredroleList;

            var compList = _masters.GetCompanies();
            var filteredCompanies = compList.Select(s => new CompanyModel
            {
                CompanyId = s.CompanyId,
                CompanyName = s.CompanyName,
            }).ToList();
            ViewBag.compDDL = filteredCompanies;

            ViewBag.UserList = _masters.GetUsersList();
            //

            userModel.CreatedDate = DateTime.Now;
            userModel.CreatedBy = Convert.ToInt32(ViewBag.Id);
            var company = compList.FirstOrDefault(c => c.CompanyId == userModel.CompanyId);
            userModel.Email = company.CompanyEmail;
            userModel.Username = company.CompanyName;
            userModel.Password = Convert.ToDateTime(company.CompRegistrationDate).ToString("ddMMyy");
            userModel.isActive = true;

            string msg = "";
            bool result = _masters.InsertModel(userModel);
            if (result == true)
            {
                msg = "Record Inserted. Company Username is its Email Id and Password is "+ userModel.Password;
                TempData["MsgScs"] = $"showSuccessPopup('{msg}');";
            }
            else
            {
                msg = "Oops! Error while inserting...";
                TempData["MsgScs"] = $"showErrorPopup('{msg}');";
            }

            return View("ManageCredential");
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
            ViewBag.StatesDDL = _masters.GetStateRecords().Where(s => s.StateStatus == true);
            ViewBag.CitiesDDL = _masters.GetCityRecords(null).Where(s => s.CityStatus == true); ;
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
            return View("StateCityLocation");
        }

        [HttpPost]
        public IActionResult UpdateStatus(string tableName, string idColumn, int id, string stColumn, bool status)
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

            var states = _masters.GetStateRecords().Where(s => s.StateStatus == true);
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
            var states = _masters.GetStateRecords().Where(s => s.StateStatus == true);
            var filteredStates = states.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName
            }).ToList();

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

        #region ==> Assign Work

        public IActionResult AddAppliances()
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
            //Dropdown Fill
            var states = _masters.GetStateRecords().Where(s => s.StateStatus == true);
            var filteredStates = states.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName
            }).ToList();
            ViewBag.stateList = filteredStates;
            var compList = _masters.GetCompanies();

            var filteredCompanies = compList.Select(s => new CompanyModel
            {
                CompanyId = s.CompanyId,
                CompanyName = s.CompanyName,
            }).ToList();
            ViewBag.compList = filteredCompanies;
            //

            ViewBag.applianceList = _masters.GetAppliances();


            return View();
        }
        [HttpPost]
        public IActionResult AddAppliances(ApplianceModel appModel)
        {
            if (HttpContext.Session.GetString("_username") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
            }
            //Dropdown Fill
            var states = _masters.GetStateRecords().Where(s => s.StateStatus == true);
            var filteredStates = states.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName
            }).ToList();
            ViewBag.stateList = filteredStates;
            var compList = _masters.GetCompanies();
            var filteredCompanies = compList.Select(s => new CompanyModel
            {
                CompanyId = s.CompanyId,
                CompanyName = s.CompanyName,
            }).ToList();
            //

            if (appModel != null)
            {
                string msg = "";
                appModel.CreatedDate = DateTime.Now;
                //ReqStatus 0 - Pending 1-Accepted 2-Rejected
                bool result = _masters.InsertModel(appModel);
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

            return View("AddAppliances");
        }

        [HttpPost]
        public IActionResult AssignWork(int companyId, int applianceId)
        {
            if (HttpContext.Session.GetString("_username") != null && HttpContext.Session.GetString("_userid") != null)
            {
                ViewBag.Data = HttpContext.Session.GetString("_username").ToString();
                ViewBag.Id = HttpContext.Session.GetString("_userid").ToString();
            }

            AssignWorkModel model = new AssignWorkModel
            {
                CompanyId = companyId,
                ApplianceId = applianceId,
                CreatedDate = DateTime.Now,
                CreadtedBy = Convert.ToInt32(ViewBag.Id),
                WorkStatus = "Not Accepted",
                //ReqStatus 0 - Pending 1-Accepted 2-Rejected
                ReqStatus = 0
            };

            var result = _db.AssignWorkMaster.Add(model);
            if (result == null)
            {
                return NotFound();
            }

            _db.SaveChanges();

            return Ok(new { success = true });
        }

        #endregion
    }
}
