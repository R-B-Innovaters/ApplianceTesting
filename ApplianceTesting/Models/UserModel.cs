using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }     
        public int? RoleId { get; set; }     
        public bool isActive { get; set; }     
    }
}
