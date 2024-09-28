using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyNumber { get; set; }
        public string? ContactNo { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyAddress { get; set; }
        public int LocationId { get; set; }
        public bool CompanyStatus { get; set; }
        public DateTime? CompRegistrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }



    }
}
