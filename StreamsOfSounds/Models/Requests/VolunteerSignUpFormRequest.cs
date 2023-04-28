using StreamsOfSound.Models.Domain_Entities;

namespace StreamsOfSound.Models.Requests
{
    public class VolunteerSignUpFormRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int InstrumentSlotsId { get; set; }

        public InstrumentSignUp ToSignUp()
        {
            return new InstrumentSignUp
            {
                UserId = UserId,
                InstrumentSlotsId = InstrumentSlotsId,
            };
        }
    }
}
