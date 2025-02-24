using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Forum.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date de création")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public int? MessageId { get; set; }
        public Message Message { get; set; }

        public int? ResponseId { get; set; }
        public Response Response { get; set; }

        [CustomValidation(typeof(Like), "ValidateLikeTarget")]
        public void ValidateLikeTarget(ValidationContext context)
        {
            if (MessageId == null && ResponseId == null)
            {
                throw new ValidationException("Un like doit être associé soit à un message soit à une réponse");
            }
            if (MessageId != null && ResponseId != null)
            {
                throw new ValidationException("Un like ne peut pas être associé à la fois à un message et à une réponse");
            }
        }
    }

}