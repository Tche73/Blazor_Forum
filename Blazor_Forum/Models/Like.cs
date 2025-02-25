using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Forum.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public int ReponseId { get; set; }

        [Required]
        public int UserId { get; set; }

        public Reponse Reponse { get; set; }

        public User User { get; set; }
    }

}