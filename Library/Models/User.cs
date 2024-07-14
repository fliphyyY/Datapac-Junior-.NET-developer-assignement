using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class User : IdentityUser<int>
    {

        public DateTime CreatedAt { get; set; }

        public ICollection<BorrowedBook> BorrowedBooks { get; set; }



    }
}
