using ApplianceTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplianceTesting.DataAccessLayer
{
    public class ApplianceTestingDBContext :DbContext
    {
        public ApplianceTestingDBContext(DbContextOptions<ApplianceTestingDBContext> options) : base(options)
        {

        }

        public DbSet<StateModel> StateMaster { get; set; }
        public DbSet<CityModel> CityMaster { get; set; }
        public DbSet<LocationModel> LocationMaster { get; set; }
        public DbSet<UserModel> UserMaster { get; set; }
    }

}
