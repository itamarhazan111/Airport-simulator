using System.ComponentModel.DataAnnotations;

namespace UserLogin.Dtos
{
    public class DtoEdit
    {
        [MaxLength(16)]
        [MinLength(3)]
        [Required]
        public string? Name { get; set; }
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
