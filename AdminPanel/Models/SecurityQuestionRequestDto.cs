using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class SecurityQuestionRequestDto
    {
        [Key]
        public int SecurtyQstnId { get; set; }
        [Required]
        public string? SecurtyQstn { get; set; }
    }
}
