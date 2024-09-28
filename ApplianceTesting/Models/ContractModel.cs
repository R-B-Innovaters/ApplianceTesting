using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class ContractModel
    {
        [Key]
        public int ContractId { get; set; } 
        public DateTime? ContractStartDate { get; set; } 
        public DateTime? ContractEndDate { get; set; }
        public int ContractPeriod { get; set; } 
        public string? ContractStatus { get; set; } 
        public string? CreatedBy { get; set; } 
        public DateTime? CreatedDate { get; set; } 
        public string? ModifiedBy { get; set; } 
        public int CompanyId { get; set; } 
    }

}
