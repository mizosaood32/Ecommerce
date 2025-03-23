using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ActionRequest
{
    public class UpdateActionGenreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }
    }
}
