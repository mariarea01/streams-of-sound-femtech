using StreamsOfSound.Models.Domain_Entities;

namespace StreamsOfSound.Models.ViewModel
{
    public class OpportunityStaffListViewModel
    {
        public List<Opportunity>Opportunity {get; set;}
        public List<InstrumentsSlots> Slots {get; set;}
        public List<InstrumentSignUp> SignUp {get; set;}    
    }
}
