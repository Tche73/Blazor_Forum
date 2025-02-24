using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Forum.Models
{
    public class Response
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "La réponse doit contenir entre 5 et 2000 caractères")]
        [Display(Name = "Contenu")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Date de création")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "L'ID du message parent est requis")]
        public int MessageId { get; set; }

        public Message Message { get; set; }
        public List<Like> Likes { get; set; } = new();
    }
}