using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;

namespace StreamsOfSound.Models.Domain_Entities
{
    public partial class SignUpForOpportunity
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int OpportunityId { get; set; }
        public ApplicationUser User { get; set; }
        public Opportunity Opportunity { get; set; }
    }
}
