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
        List<LoginModel> CheckLogin(string usr, string paswd);
    }
    public interface IMasterControl
    {
        //string AddState(StateModel objStateMOdel);
        string CommonInsertOperation(object objModel,string tblName);

        public List<Dictionary<string, object>> GetRecords(string tableName, string columnList, string join = null, string condition = null);
    }
    //public interface IRestaurant
    //{
    //    //RESTAURANT REGISTRATION
    //    List<Restaurant_Info_Model> GetRestaurants();
    //    string AddRestaurant(Restaurant_Info_Model objRestaurantInfo);

    //    Restaurant_Info_Model GetRestaurantById(Guid id);
    //    string UpdateRestaurant(Restaurant_Info_Model objRestaurantInfo);
    //    void DeleteRestaurant(Guid id);

    //}
}
