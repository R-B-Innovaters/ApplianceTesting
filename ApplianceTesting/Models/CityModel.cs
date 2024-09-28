using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool CityStatus { get; set; }
    }
}
