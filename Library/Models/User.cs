using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class User : IdentityUser<int>
    {


        public ICollection<BorrowedBooks> BorrowedBooks { get; set; }
        
        public DateTime CreatedAt { get; set; }


    }
}
