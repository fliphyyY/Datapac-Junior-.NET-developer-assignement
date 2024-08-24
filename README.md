# **Task: Implementation of API endpoints for managing books in the library**

Your task is to create API endpoints for managing borrowed books.

## **Requirements:**

1. Create an ASP.NET Core application for managing borrowed books.
2. Use database models for users and books and a data layer to enable communication with the database (eg using Entity Framework Core).
3. Endpoints should support:

   * Creating a new book
   * Get the details of an existing book by ID
   * Updating an existing book
   * Deleting a book
   * Creating a new loan
   * Confirmation of the return of the borrowed book
   * Ensure input data validation
  
5. Ensure input data validation.
   
## **Bonus Quests:**

   * Code coverage with tests (unit tests, integration tests)
   * Implements the functionality of automatically sending a reminder to the user a day before the deadline for returning the book (fake sending email).

## **Note for evaluation:**

Not only functionality and proper functioning of API endpoints will be evaluated, but also code quality, adherence to best practices, project structure, and the ability to explain and defend decisions in the design and implementation of the solution.

# My Solution

 Simple backend-only application for administration of borrowed books. 

ASP.NET Core 8 with MSSQL database and Entity Framework is used. The data model has three tables: Book, User, and BorrowedBook. User inherits from IdentityUser.

## API

The LibraryController has six endpoints:

* Creating a new book
* Get the details of an existing book by ID
* Updating an existing book
* Deleting a book
* Borrowing a book
* Confirmation of the return of the borrowed book
