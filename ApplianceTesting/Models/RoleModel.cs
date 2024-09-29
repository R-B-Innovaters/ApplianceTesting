using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool RoleStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
