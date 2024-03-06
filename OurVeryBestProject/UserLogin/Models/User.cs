using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserLogin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
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
        [Required(ErrorMessage = "IsAdmin is required")]
        [Range(0,1)]
        public int IsAdmin { get; set; }
        public string? CodeForPassword { get; set; }

    }
}
