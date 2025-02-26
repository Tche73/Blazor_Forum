using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Le contenu doit contenir entre 10 et 5000 caractères")]
        [Display(Name = "Contenu")]
        public string Contenu { get; set; }

        [Required]
        [Display(Name = "Date de publication")]
        [DataType(DataType.DateTime)]
        public DateTime DatePublication { get; set; }

        [Required(ErrorMessage = "L'utilisateur est obligatoire")]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Reponse> Reponses { get; set; } = new();
    }

}