using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamsOfSounds.Models
{
    public class VolunteerSignUp
    {
        
    [Display(Name = "Serial No")]
        public byte EmployeeId { get; set; }

        [Display(Name = "Name")]
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
