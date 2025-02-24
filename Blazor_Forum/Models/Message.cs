using Azure;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 100 caractères")]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Le contenu doit contenir entre 10 et 5000 caractères")]
        [Display(Name = "Contenu")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Date de création")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
       
        public List<Response> Responses { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
    }

}