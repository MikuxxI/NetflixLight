
namespace user_service.Controllers.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = "";

        public string Lastname { get; set; } = "";

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public double Sold { get; set; }

        public bool AdminRole { get; set; }
    }
}
