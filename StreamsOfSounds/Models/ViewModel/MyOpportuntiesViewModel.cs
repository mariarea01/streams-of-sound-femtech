namespace StreamsOfSound.Models.ViewModel
{
    public class MyOpportuntiesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Address { get; set; }
        public string? Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }   
        public string Zip { get; set; } 
        public int signUpId { get; set; }
        public string Instrument { get; set; }
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }   
    }
}
