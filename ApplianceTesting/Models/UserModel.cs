using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }     
        public int? RoleId { get; set; }     
        public bool isActive { get; set; }     
        public int? CompanyId { get; set; }     
        public DateTime? CreatedDate { get; set; }     
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
