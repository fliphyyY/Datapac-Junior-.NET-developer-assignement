# Library

 Simple backend only application for administration of borrowed books. 

ASP.NET Core 8 with MSSQL database and Entity Framework is used. The data model has three tables: Book, User, and BorrowedBook. User inherits from IdentityUser.

## API

The LibraryController has six endpoints:

* Creating a new book
* Get the details of an existing book by ID
* Updating an existing book
* Deleting a book
* Creating a new loan
* Confirmation of the return of the borrowed book
