using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int CityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool LocationStatus { get; set; } 
    }
}
