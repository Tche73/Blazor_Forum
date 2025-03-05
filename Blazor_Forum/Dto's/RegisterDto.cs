using System.ComponentModel.DataAnnotations;

namespace Blazor_Forum.Dto_s
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom d'utilisateur doit avoir entre 3 et 50 caractères")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit avoir entre 6 et 100 caractères")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; }
    }


}
