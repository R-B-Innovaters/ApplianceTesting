using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int CityId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        //public CityModel  cityModel  { get; set; }
        //public StateModel stateModel { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}
