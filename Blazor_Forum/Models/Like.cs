using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "L'identifiant de la réponse est obligatoire")]
        public int ReponseId { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est obligatoire")]
        public int UserId { get; set; }

        public Reponse Reponse { get; set; }

        public User User { get; set; }
    }

}