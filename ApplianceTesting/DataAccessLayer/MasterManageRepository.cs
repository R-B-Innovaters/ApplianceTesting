using ApplianceTesting.Models;
using Microsoft.EntityFrameworkCore;
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
            var stateList = _db.StateMaster.Select(s => new { s.StateId, s.StateName }).ToList();
            List<StateModel> sList = stateList.Select(s => new StateModel
            {
                StateId = s.StateId,
                StateName = s.StateName
            }).ToList();
            return sList;
        }

        public List<CityModel> GetCityRecords(int stateId)
        {
            var cityList = _db.CityMaster.Select(s => new { s.CityId, s.CityName,s.StateId }).Where(s=>s.StateId == stateId).ToList();
            List<CityModel> cList = cityList.Select(s => new CityModel
            {
                CityId = s.CityId,
                CityName = s.CityName
            }).ToList();

            return cList;
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
        public List<LocationModel> GetStateCityLocRecords()
        {
            var locations = from l in _db.LocationMaster
                            join c in _db.CityMaster on l.CityId equals c.CityId
                            join s in _db.StateMaster on c.StateId equals s.StateId
                            select new 
                            {
                             l.LocationName,
                             c.CityName,
                             s.StateName
                            };
            List<LocationModel> lList = locations.Select(s => new LocationModel
            {
                LocationName = s.LocationName,
                CityName = s.CityName,
                StateName = s.StateName,
            }).ToList();
            return lList;
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


    }

}
