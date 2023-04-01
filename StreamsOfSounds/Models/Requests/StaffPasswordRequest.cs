namespace StreamsOfSounds.Models.Requests
{
    public class StaffPasswordRequest 
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        public string Token { set; get; }
    }
}
