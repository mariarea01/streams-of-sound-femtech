using StreamsOfSound.Models.Domain_Entities;

namespace StreamsOfSound.Models.Requests
{
    public class CreateOpportunityRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public int SlotsOpenings { get; set; }
        public int SlotsAvailable { get; set; }
        public List<InstrumentsSlots> Slots { get; set; }
        public int Id { get; set; } 

        public Opportunity ToOpportunity()
        {
            return new Opportunity
            {
                Name = Name,
                Description = Description,
                StartTime = StartTime,
                EndTime = EndTime,
                Address = Address,
                Address1 = Address1,
                City = City,
                State = State,
                Zip = Zip,
                SlotsOpenings = SlotsOpenings,
                SlotsAvailable = SlotsAvailable,
            };
        }
    }
}
