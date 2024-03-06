using System.ComponentModel.DataAnnotations;

namespace UserLogin.Dtos
{
    public class DtoVerifyCode
    {
        [MinLength(5)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? code { get; set; }
    }
}
