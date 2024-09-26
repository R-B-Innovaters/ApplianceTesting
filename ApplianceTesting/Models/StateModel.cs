using System.ComponentModel.DataAnnotations;

namespace ApplianceTesting.Models
{
    public class StateModel
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }


    }
}