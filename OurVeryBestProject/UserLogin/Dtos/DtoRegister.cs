using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserLogin.Dtos
{
    public class DtoRegister
    {
        [MaxLength(16)]
        [MinLength(3)]
        [Required]
        public string? Name { get; set; }
        [MinLength(5)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        [Range(0, 1)]
        public int IsAdmin { get; set; }
        [MaxLength(20)]
        [MinLength(8)]
        [Required]
        public string? Password { get; set; }

    }
}
