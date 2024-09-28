using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class StateModel
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool StateStatus { get; set; }

    }
}