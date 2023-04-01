﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamsOfSounds.Models
{
    public class CreateNewStaff : ApplicationUser
    {
        [DisplayName("First Name")]
        public String FirstName { get; set; }

        [DisplayName("Position")]
        public String positionInCompany { get; set; }

        [DisplayName("Last Name")]
        public String LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public String Password { get; set; }    
  
    }
}
