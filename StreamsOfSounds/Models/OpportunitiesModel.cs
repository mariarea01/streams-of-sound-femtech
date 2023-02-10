using System.ComponentModel.DataAnnotations;

namespace VolunteerWebApplication.Models
{
    public class OpportunitiesModel
    {
        public OpportunitiesModel()
        {
        }
        public string? EventName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }
        public string? Duration { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? NumOfVolunteers { get; set; }
        public bool? Paid { get; set; }
        public bool? Unpaid { get; set; }
        public string? PaidAmount { get; set; }
        public bool? SignUp { get; set; }
    }
}

