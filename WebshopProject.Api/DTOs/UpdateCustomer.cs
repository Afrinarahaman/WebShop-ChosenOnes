using System.ComponentModel.DataAnnotations;
using WebshopProject.Api.Helpers;

namespace WebshopProject.Api.DTOs
{
    public class UpdateCustomer
    {
        [Required]
        [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Password must be less than 32 chars")]
        public string Password { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "FirstName must be less than 32 chars")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "LastName must be less than 32 chars")]
        public string LastName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Address must be less than 32 chars")]
        public string Address { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Telephone must be less than 32 chars")]
        public string Telephone { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
