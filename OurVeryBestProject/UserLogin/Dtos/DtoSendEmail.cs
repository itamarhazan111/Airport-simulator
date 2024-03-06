using System.ComponentModel.DataAnnotations;

namespace UserLogin.Dtos
{
    public class DtoSendEmail
    {
        [MinLength(5)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
    }
}
