using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class ListViewModel
    {
    }
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public bool LocationStatus { get; set; }
    }
    public class CityViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public bool CityStatus { get; set; }
    }
    public class ApplianceViewModel
    {
        public int ApplianceId { get; set; }
        public string? ApplianceName { get; set; }
        public string? AppSerialNumber { get; set; }
        public string? Barcode { get; set; }
        public string? LocationName { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public int? ReqStatus { get; set; }
        public int? CompanyId { get; set; }

    }

    public class UserViewModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public bool? isActive { get; set; }
       
    }
}

