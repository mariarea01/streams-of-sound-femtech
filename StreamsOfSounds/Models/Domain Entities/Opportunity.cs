namespace StreamsOfSounds.Models.Domain_Entities
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DateTime { get; set; }
        public int Duration { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip {get; set; }
        public string State { get; set; }
        public int SlotsOpenings { get; set; }
        public int SlotsAvailable  { get; set; }
        public int UserId { get; set; }

    }
}
