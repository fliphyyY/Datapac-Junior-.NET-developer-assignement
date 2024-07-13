using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BorrowedBookDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BorrowedBookId { get; set; }

    }
}
