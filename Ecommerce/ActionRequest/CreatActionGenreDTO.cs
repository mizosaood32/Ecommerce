using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ActionRequest
{
    public class CreatActionGenreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }
    }
}
