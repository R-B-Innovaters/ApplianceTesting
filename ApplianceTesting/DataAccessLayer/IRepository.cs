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
        public List<StateModel> GetStateRecords();
        public List<CityModel> GetCityRecords(int stateId);
        bool InsertModel(object objModel);
        public List<NumCounts> GetCounts();
        public List<LocationModel> GetStateCityLocRecords();
    }
}
