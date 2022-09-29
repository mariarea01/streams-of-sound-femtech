using System.ComponentModel.DataAnnotations;

namespace StreamsOfSounds.Models
{
    public class EmpModel
    {
        /// <summary>  
        /// DOB datetime data type property   
        /// to display date type control  
        /// </summary>  
        [Display(Name = "Pick a day to volunteer")]
        [DataType(DataType.Date)]
        public DateTime? DOV { get; set; }

        public string Name { get; set; }
        public string Instrument { get; set; }
        public string Email { get; set; }

    }
}
