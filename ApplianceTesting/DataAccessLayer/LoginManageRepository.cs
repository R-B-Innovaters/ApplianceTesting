using ApplianceTesting.Models;
using System.Data;
using System.Data.SqlClient;

namespace ApplianceTesting.DataAccessLayer.Repository
{
    public class LoginManageRepository : IHomeControl
    {
        private readonly IWebHostEnvironment _hostEnvirionment;
        //string conString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
      
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public LoginManageRepository( IWebHostEnvironment hostEnvirionment)
        {
            _hostEnvirionment = hostEnvirionment;
        }
        public string getConnection()
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];

            return dbconnectionStr;
        }

        public List<LoginModel> CheckLogin(string usr, string paswd)
        {
            try
            {
                List<LoginModel> userList = new List<LoginModel>();
                string dbconnectionStr = getConnection().ToString();

                using (SqlConnection con = new SqlConnection(dbconnectionStr))
                {
                    string query = "SELECT *FROM UserMaster WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", usr);
                        cmd.Parameters.AddWithValue("@Password", paswd);

                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                userList.Add(new LoginModel
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Username = Convert.ToString(sdr["Username"]),
                                    Password = Convert.ToString(sdr["Password"])
                                });
                            }
                        }

                        con.Close();
                    }
                }

                return userList;
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }
        //public List<LoginModel> CheckLogin()
        //{
        //    try
        //    {
        //        var restList = _db.RestaurantInfoTable.Where(s => s.restaurantisActive == true).ToList();
        //        _log4net.Info("Record Fetched from RestaurantInfoTable through Restaurant Repository");
        //        return restList;
        //    }
        //    catch
        //    {
        //        _log4net.Error("Error when record fetched from RestaurantInfoTable through Restaurant Repository");
        //        throw;
        //    }
        //}

        //Add Restaurants Details
        //public string AddRestaurant(Restaurant_Info_Model objRestaurantInfo)
        //{
        //    try
        //    {
        //        //var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        //        objRestaurantInfo.restaurantid = Guid.NewGuid();
        //        objRestaurantInfo.restaurantinsertdatetime = DateTime.Now;
        //        objRestaurantInfo.restaurantisActive = true;
        //        _db.RestaurantInfoTable.Add(objRestaurantInfo);

        //        _db.SaveChanges();
        //        _log4net.Info("Record Inserted in RestaurantInfoTable through Restaurant Repository");

        //        return "Record Inserted";

        //    }
        //    catch (Exception ex)
        //    {
        //        _log4net.Error("Error when record inserted in RestaurantInfoTable through Restaurant Repository");
        //        return Convert.ToString(ex);
        //    }
        //}

        //Get Id
        //public Restaurant_Info_Model GetRestaurantById(Guid id)
        //{
        //    try
        //    {
        //        Restaurant_Info_Model restaurant = _db.RestaurantInfoTable.FirstOrDefault(s => s.restaurantid.Equals(id));
        //        _log4net.Info("Fetch perticular restaurant id from RestaurantInfoTable through Restaurant Repository");
        //        return restaurant;

        //    }
        //    catch
        //    {
        //        _log4net.Error("Error when Fetch perticular restaurant id from RestaurantInfoTable through Restaurant Repository");
        //        throw;
        //    }
        //}

        //Update Restaurants Details
        //public string UpdateRestaurant(Restaurant_Info_Model objRestaurantInfo)
        //{
        //    try
        //    {
        //        objRestaurantInfo.restaurantupdatedatetime = DateTime.Now;
        //        _db.RestaurantInfoTable.Update(objRestaurantInfo);
        //        _db.SaveChanges();
        //        _log4net.Info("Record updated in RestaurantInfoTable through Restaurant Repository");
        //        return "Record Updated";
        //    }
        //    catch
        //    {
        //        _log4net.Error("Error when record updated in RestaurantInfoTable through Restaurant Repository");
        //        throw;
        //    }
        //}

        //Delete Restaurants Details     
        //public void DeleteRestaurant(Guid id)
        //{
        //    try
        //    {
        //        Restaurant_Info_Model objRestaurantInfo = _db.RestaurantInfoTable.Find(id);
        //        objRestaurantInfo.restaurantdeletestatus = true;
        //        _db.RestaurantInfoTable.Update(objRestaurantInfo);
        //        _db.SaveChanges();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }

}
