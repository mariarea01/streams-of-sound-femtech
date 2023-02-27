using StreamsOfSound.Models.Domain_Entities;

namespace StreamsOfSound.Models.Requests
{
    // TODO: Consider having a base class to inherit from
    public class CreateOpportunityRequest
    {
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public int NumOfVolunteers { get; set; }
        public bool Paid { get; set; }
        public bool Unpaid { get; set; }
        public decimal? PaidAmount { get; set; }


        public Opportunity ToOpportunity()
        {
            return new Opportunity
            {
                Name = EventName,
                Description = Description,
                StartDateTimeUtc = StartTime,
                EndDateTimeUtc = EndTime,
                Address = Address,
                Address1 = Address1,
                City = City,
                State = State,
                Zip = Zip
                // TODO: etc...
            };
        }
    }
}
