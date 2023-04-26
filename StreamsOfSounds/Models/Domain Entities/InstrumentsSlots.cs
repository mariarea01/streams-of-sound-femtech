namespace StreamsOfSound.Models.Domain_Entities
{
    public class InstrumentsSlots
    {
        public int Id { get; set; }
        public string Instrument { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
        public int? OpportunityId { get; set; }  
    }
}
