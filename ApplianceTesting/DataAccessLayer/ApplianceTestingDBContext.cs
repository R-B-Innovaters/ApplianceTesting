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
        public DbSet<CompanyModel> CompanyMaster { get; set; }
        public DbSet<ContractModel> ContractMaster { get; set; }
        public DbSet<ApplianceModel>ApplianceMaster { get; set; }
        public DbSet<AssignWorkModel>AssignWorkMaster { get; set; }
        public DbSet<RoleModel>RoleMaster { get; set; }
    }

}
