namespace Library.Models
{
    public class BorrowedBooks
    {
            public int BorrowId { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
            public int BookId { get; set; }
            public Book Book { get; set; }
            public DateTime BorrowedDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnedDate { get; set; }
        
    }
}
