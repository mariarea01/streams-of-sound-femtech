namespace StreamsOfSound.Models.ViewModel
{
    public class ViewOpportunityViewModel
    {
        public string OppName { get; set; }
        public DateTimeOffset OppStartTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string SlotInstrument { get; set; }
        public DateTime SlotStartTime   { get; set; }
        public DateTime SlotEndTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
