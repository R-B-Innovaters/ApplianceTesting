using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class ApplianceModel
    {
        [Key]
        public int ApplianceId { get; set; }
        public string ApplianceName { get; set; }
        public string AppSerialNumber { get; set; }
        public string Barcode { get; set; }
        public int? LocationId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool AppStatus { get; set; }
    }
}
