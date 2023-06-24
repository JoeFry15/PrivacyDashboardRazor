using System.ComponentModel.DataAnnotations;

namespace PrivacyDashboardRazor.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string? Website { get; set; }

        [Required]
        public string? Email { get; set; }

    }
}
