using Azure;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<Message> Messages { get; set; } = new();

        public List<Response> Reponses { get; set; } = new();

        public List<Like> Likes { get; set; } = new();
    }
}
