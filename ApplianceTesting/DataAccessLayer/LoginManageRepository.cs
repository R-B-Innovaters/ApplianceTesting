using ApplianceTesting.Models;
using System.Data;
using System.Data.SqlClient;

namespace ApplianceTesting.DataAccessLayer.Repository
{
    public class LoginManageRepository : IHomeControl
    {
        private readonly IWebHostEnvironment _hostEnvirionment;
        private readonly ApplianceTestingDBContext _db;

        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public LoginManageRepository(IWebHostEnvironment hostEnvirionment, ApplianceTestingDBContext db)
        {
            _hostEnvirionment = hostEnvirionment;
            _db = db;
        }


        public UserModel GetUser(string username, string password)
        {
            try
            {
                UserModel userModel = _db.UserMaster.Where(u=>u.Username==username && u.Password==password).FirstOrDefault();
               
                return userModel;
            }
            catch
            {
                throw;
            }
        }

    }

}
