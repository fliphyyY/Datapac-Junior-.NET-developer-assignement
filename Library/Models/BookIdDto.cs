using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookIdDto
    {
        [Required]
        public int BookId { get; set; }
    }
}
