using ApplianceTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ApplianceTesting.DataAccessLayer.Repository
{
    public interface IRepository
    {
    }
    public interface IHomeControl
    {
        UserModel GetUser(string username, string password);
    }

    public interface IMasterControl
    {
        public List<RoleModel> GetRole();
        public List<StateModel> GetStateRecords();
        public List<CityViewModel> GetCityRecords(int? stateId);
        public List<LocationModel> GetLocationRecords(int cityId);
        bool InsertModel(object objModel);
        public List<NumCounts> GetCounts();
        public List<LocationViewModel> GetStateCityLocRecords();
        public List<ApplianceViewModel> GetAppliances();
        public List<CompanyModel> GetCompanies();
        public bool UpdateStatusModel(string dbSetName, string idField, int id, string statusField, bool statusValue);
    }
}
