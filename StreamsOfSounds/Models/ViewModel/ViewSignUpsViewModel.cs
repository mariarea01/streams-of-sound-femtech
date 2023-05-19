namespace StreamsOfSound.Models.ViewModel
{
    public class ViewSignUpsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }   
        public string OppName { get; set; }
        public DateTimeOffset OppStartTime { get; set; }
        public string Instrument { get; set; }
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
    }
}
