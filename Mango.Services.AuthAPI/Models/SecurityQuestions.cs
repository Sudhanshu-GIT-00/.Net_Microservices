using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models
{
    public class SecurityQuestions
    {
        [Key]
        public int SecurtyQstnId { get; set; }
        [Required]
        public string? SecurtyQstn { get; set; }
    }
}
