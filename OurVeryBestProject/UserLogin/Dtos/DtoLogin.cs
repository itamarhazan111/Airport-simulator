

using System.ComponentModel.DataAnnotations;

namespace UserLogin.Dtos
{
    public class DtoLogin
    {
        [MinLength(5)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [MaxLength(20)]
        [MinLength(8)]
        [Required]
        public string? Password { get; set; }
    }
}
