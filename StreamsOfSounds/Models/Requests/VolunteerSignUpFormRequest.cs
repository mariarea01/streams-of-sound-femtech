using StreamsOfSound.Models.Domain_Entities;

namespace StreamsOfSound.Models.Requests
{
    public class VolunteerSignUpFormRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int OpportunityId { get; set; }

        public SignUpForOpportunity ToSignUp()
        {
            return new SignUpForOpportunity
            {
                UserId = UserId,
                OpportunityId = OpportunityId,
                Id = Id,
            };
        }
    }
}
