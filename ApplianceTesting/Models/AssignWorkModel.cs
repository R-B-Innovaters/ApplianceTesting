using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class AssignWorkModel
    {
        [Key]
        public int AssignWorkId { get; set; }
        public int? ApplianceId { get; set; }
        public int? CompanyId { get; set; }
        public string? WorkStatus { get; set; }
        public int? ReqStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreadtedBy { get; set; }
    }
}
