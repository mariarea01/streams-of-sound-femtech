using StreamsOfSound.Models.Domain_Entities;
using StreamsOfSounds.Models;


namespace StreamsOfSound.Models.ViewModel
{
    public class ConfirmSignUpViewModel
    {
        public ApplicationUser User { get; set; }
        public Opportunity Opportunity { get; set; }

        public Guid UserId { get; set; }
        public int OpportunityId { get; set; }
    }
}
