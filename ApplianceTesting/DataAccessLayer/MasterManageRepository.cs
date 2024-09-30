using ApplianceTesting.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;

namespace ApplianceTesting.DataAccessLayer.Repository
{
    public class MasterManageRepository : IMasterControl
    {
        private readonly IWebHostEnvironment _hostEnvirionment;
        private readonly ApplianceTestingDBContext _db;

        public MasterManageRepository(IWebHostEnvironment hostEnvirionment, ApplianceTestingDBContext db)
        {
            _hostEnvirionment = hostEnvirionment;
            _db = db;
        }
        public List<RoleModel> GetRole()
        {
            var roleList = _db.RoleMaster.Select(s => new { s.RoleId, s.RoleName, s.RoleStatus }).ToList();
            List<RoleModel> rList = roleList.Select(s => new RoleModel
            {
                RoleId = s.RoleId,
                RoleName = s.RoleName,
                RoleStatus = s.RoleStatus
            }).ToList();
            return rList;
        }
        public List<StateModel> GetStateRecords()
        {
            //// Mapping StateMaster to StateModel if necessary
            //var r = await _db.StateMaster.ToListAsync();
            //List<StateModel> sList = r.Select(s => new StateModel
            //{
            //  StateId = s.StateId,    
            //   StateName = s.StateName 
            //}).ToList();
            //return sList;
            var stateList = _db.StateMaster.Select(s => new { s.StateId, s.StateName, s.StateStatus }).ToList();
            List<StateModel> sList = stateList.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName,
                StateStatus = s.StateStatus
            }).ToList();
            return sList;
        }

        public List<CityViewModel> GetCityRecords(int? stateId)
        {
            if (stateId != null)
            {
                var cityList = _db.CityMaster.Select(s => new { s.CityId, s.CityName, s.StateId, s.CityStatus }).Where(s => s.StateId == stateId).ToList();
                List<CityViewModel> cList = cityList.Select(s => new CityViewModel
                {
                    CityId = s.CityId,
                    CityName = s.CityName,
                    CityStatus = s.CityStatus
                }).ToList();
                return cList;
            }
            else
            {
                var cityList = (from ct in _db.CityMaster
                                join state in _db.StateMaster on ct.StateId equals state.StateId
                                select new
                                {
                                    ct.CityId,
                                    ct.CityName,
                                    ct.CityStatus,
                                    state.StateName
                                }).ToList();
                List<CityViewModel> cList = cityList.Select(s => new CityViewModel
                {
                    CityId = s.CityId,
                    CityName = s.CityName,
                    StateName = s.StateName,
                    CityStatus = s.CityStatus
                }).ToList();
                return cList;
            }

        }
        public List<LocationModel> GetLocationRecords(int cityId)
        {
            var locationList = _db.LocationMaster.Select(s => new { s.LocationId, s.LocationName, s.CityId,s.LocationStatus }).Where(s => s.CityId == cityId).ToList();
            List<LocationModel> lList = locationList.Select(s => new LocationModel
            {
                LocationId = s.LocationId,
                LocationName = s.LocationName,
                LocationStatus=s.LocationStatus
            }).ToList();

            return lList;
        }
        public List<NumCounts> GetCounts()
        {
            int stateCount = _db.StateMaster.Count();
            int cityCount = _db.CityMaster.Count();
            int locationCount = _db.LocationMaster.Count();

            NumCounts counts = new NumCounts
            {
                TotalStates = stateCount,
                TotalCities = cityCount,
                TotalLocations = locationCount
            };

            return new List<NumCounts> { counts };
        }
        public List<LocationViewModel> GetStateCityLocRecords()
        {
            var locations = (from loc in _db.LocationMaster
                             join city in _db.CityMaster on loc.CityId equals city.CityId
                             join state in _db.StateMaster on city.StateId equals state.StateId
                             select new LocationViewModel
                             {
                                 LocationId = loc.LocationId,
                                 LocationName = loc.LocationName,
                                 CityName = city.CityName,
                                 StateName = state.StateName,
                                 LocationStatus = loc.LocationStatus
                             }).ToList();

            return locations;
        }
        public List<UserViewModel> GetUsersList()
        {
            var users = (from user in _db.UserMaster
                             join role in _db.RoleMaster on user.RoleId equals role.RoleId
                             select new UserViewModel
                             {
                                 UserId = user.UserId,
                                 Username=user.Username,
                                 RoleName = role.RoleName,
                                 isActive= user.isActive,
                                 Email=user.Email,
                                 Password=user.Password
                             }).ToList();

            return users;
        }
        public List<CompanyModel> GetCompanies()
        {
            var c_list = from l in _db.CompanyMaster
                         select new
                         {
                             l.CompanyId,
                             l.CompanyName,
                             l.CompanyNumber,
                             l.CompanyEmail,
                             l.CompanyAddress,
                             l.ContactNo,
                             l.CompRegistrationDate,
                             l.CompanyStatus
                         };
            List<CompanyModel> compList = c_list.Select(s => new CompanyModel
            {
                CompanyName = s.CompanyName,
                CompanyId = s.CompanyId,
                CompanyNumber = s.CompanyNumber,
                CompanyEmail = s.CompanyEmail,
                CompanyAddress = s.CompanyAddress,
                CompanyStatus = Convert.ToBoolean(s.CompanyStatus),
                CompRegistrationDate = s.CompRegistrationDate,
                ContactNo = s.ContactNo
            }).ToList();
            return compList;
        }
        public List<ApplianceViewModel> GetAppliances()
        {
            var appliances = (from app in _db.ApplianceMaster
                              join work in _db.AssignWorkMaster on app.ApplianceId equals work.ApplianceId into workGroup
                              from work in workGroup.DefaultIfEmpty()
                              join location in _db.LocationMaster on app.LocationId equals location.LocationId
                              join city in _db.CityMaster on location.CityId equals city.CityId
                              join state in _db.StateMaster on city.StateId equals state.StateId
                              select new ApplianceViewModel
                              {
                                  ApplianceId = app.ApplianceId,
                                  ApplianceName = app.ApplianceName,
                                  AppSerialNumber = app.AppSerialNumber,
                                  LocationName = location.LocationName,
                                  CityName = city.CityName,
                                  StateName = state.StateName,
                                  ReqStatus=work.ReqStatus,
                                  CompanyId=work.CompanyId
                              }).ToList();

            return appliances;
        }
        public bool InsertModel(object objModel)
        {
            try
            {
                // Get the type of the model being passed
                Type modelType = objModel.GetType();

                // Use reflection to call the generic Set method with the model type
                var dbSet = _db.GetType().GetMethod("Set", new Type[] { })
                                .MakeGenericMethod(modelType)
                                .Invoke(_db, null);

                // Use reflection to add the object to the DbSet
                dbSet.GetType().GetMethod("Add").Invoke(dbSet, new[] { objModel });

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateStatusModel(string dbSetName, string idField, int id, string statusField, bool statusValue)
        {
            try
            {
                // Get the DbSet from the context dynamically using reflection
                var dbSetProperty = _db.GetType().GetProperty(dbSetName);
                if (dbSetProperty == null)
                {
                    Console.WriteLine($"DbSet '{dbSetName}' not found.");
                    return false;
                }

                // Get the DbSet object (of type DbSet<T>)
                var dbSet = dbSetProperty.GetValue(_db) as IQueryable;
                if (dbSet == null)
                {
                    Console.WriteLine($"Could not retrieve DbSet for '{dbSetName}'.");
                    return false;
                }

                // Switch to client-side evaluation by materializing the query first (fetch entity by ID)
                var entity = dbSet.Cast<object>()
                                  .AsEnumerable() // Forces client-side evaluation
                                  .FirstOrDefault(e => e.GetType().GetProperty(idField).GetValue(e).Equals(id));

                if (entity == null)
                {
                    Console.WriteLine($"Entity with ID {id} not found.");
                    return false;
                }

                // Use reflection to set the status field
                var property = entity.GetType().GetProperty(statusField);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(entity, statusValue);
                }
                else
                {
                    Console.WriteLine($"Property '{statusField}' not found or is not writable.");
                    return false;
                }

                // Save changes to the database
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }

}
