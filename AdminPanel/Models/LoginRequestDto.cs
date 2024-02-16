using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

