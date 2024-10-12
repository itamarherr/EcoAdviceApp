using System.ComponentModel.DataAnnotations;

namespace EcoAdviceAppApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email {  get; set; }
        [Required]
        [MinLength (2), MaxLength(20)]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        public required string Name { get; set; }
    }
}
