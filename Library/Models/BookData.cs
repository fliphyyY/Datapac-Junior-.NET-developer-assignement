using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookData
    {

        [Required]
        [RegularExpression(@"^[\w\s\p{P}]{1,100}$", ErrorMessage = "Invalid title!")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[\w\s.]{1,50}$", ErrorMessage = "Invalid author's name!")]
        public string Author { get; set; }
    }
}
