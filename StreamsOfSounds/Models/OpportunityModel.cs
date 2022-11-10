using System.ComponentModel.DataAnnotations;
namespace StreamsOfSounds.Models
{
    public class OpportunityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Instrument { get; set; }
    }
}
