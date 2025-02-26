using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Forum.Models
{
    public class Reponse
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "La réponse doit contenir entre 5 et 2000 caractères")]
        [Display(Name = "Contenu")]
        public string Contenu { get; set; }

        [Required]
        [Display(Name = "Date de publication")]
        [DataType(DataType.DateTime)]
        public DateTime DatePublication { get; set; }

        [Required(ErrorMessage = "Le message parent est obligatoire")]
        public int MessageId { get; set; }

        [Required(ErrorMessage = "L'utilisateur est obligatoire")]
        public int UserId { get; set; }

        public Message Message { get; set; }

        public User User { get; set; }

        public List<Like> Likes { get; set; } = new();
    }

}