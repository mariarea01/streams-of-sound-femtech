namespace StreamsOfSound.Models.Requests
{
    public class YeetSignUpRequest
    {
        public int OpportunityId { get; set; }
        public string ReasonToCancel { get; set; }
        public int slotId { get; set; }
        public int signUpId { get; set; }   
    }
}
