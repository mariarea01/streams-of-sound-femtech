namespace StreamsOfSound.Models.Domain_Entities
{
    public class InstrumentSignUp
    {
        public int Id { get; set; } 
        public Guid? UserId { get; set; }    
        public int? InstrumentSlotsId { get; set; }  

    }
}
