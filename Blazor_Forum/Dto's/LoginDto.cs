using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Dto_s
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit avoir entre 6 et 100 caractères")]
        public string Password { get; set; }
    }
}
