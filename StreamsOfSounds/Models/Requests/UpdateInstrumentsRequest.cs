using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamsOfSound.Models.Requests
{
    public class UpdateInstrumentsRequest
    {
        [Required]
        [Display(Name = "Current Instrument List")]
        public string OldInstruments { get; set; }

        [Required]
        [Display(Name = "Updated Instrument List")]
        public string NewInstruments { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

    }
}
