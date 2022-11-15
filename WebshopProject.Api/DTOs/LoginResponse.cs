using WebshopProject.Api.Helpers;

namespace WebshopProject.Api.DTOs
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}

