using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


    }
}
