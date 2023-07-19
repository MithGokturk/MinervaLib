using MinervaLib.Models;
using MinervaLib.Repositories;
using System.Data.OleDb;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\MithGokturk\\Documents\\MinervaLib.accdb;";
        //using (OleDbConnection connection = new(connectionString))
        //{
        //    connection.Open();
        //    Console.WriteLine("Database Connect Success");

        //}

        IBookRepository bookRepository = new BookRepository(connectionString);

        char response;

        //Book addition process
        do
        {
            Console.WriteLine("***********************");
            Console.WriteLine("1- Add New Book");
            Console.WriteLine("2- Delete Book");
            Console.WriteLine("3- List All Books");
            Console.WriteLine("4- Update Existing Book");
            Console.WriteLine("5- Exit Programme");
            Console.WriteLine("***********************");

            Console.Write("Please select the operation you want to perform: ");
            char selectedOption = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (selectedOption)
            {
                case '1':
                    Console.WriteLine("Add New Book");
                    Console.Write("Book Name: ");
                    string bookName = Console.ReadLine();

                    Console.Write("Author: ");
                    string author = Console.ReadLine();

                    Console.Write("Publisher: ");
                    string publisher = Console.ReadLine();

                    Console.Write("ISBN: ");
                    string isbn = Console.ReadLine();

                    Console.Write("Page Number: ");
                    int pageNumber = int.Parse(Console.ReadLine());

                    Book newBook = new Book(0, bookName, author, publisher, isbn, pageNumber);

                    bookRepository.AddBook(newBook);

                    Console.WriteLine("Book successfully added.");

                    Console.Write("Would you like to add a new book? (E/H): ");
                    response = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    break;

                case '2':
                    Console.Write("Enter the ID of the book you want to delete: ");
                    int bookID = int.Parse(Console.ReadLine());

                    bookRepository.DeleteBook(bookID);

                    Console.WriteLine("Book successfully deleted");
                    break;

                case '3':
                    List<Book> allBooks = bookRepository.GetAllBooks();
                    foreach (Book book in allBooks)
                    {
                        Console.WriteLine($"ID: {book.ID}, Book Name: {book.BookName}, Author: {book.Author}, Publisher: {book.Publisher}, ISBN: {book.ISBN}, Page Number: {book.PageNumber}");
                    }
                    break;

                case '4':
                    Console.Write("Enter the ID of the book you want to update: ");
                    int bookIDToUpdate = int.Parse(Console.ReadLine());

                    //Get the book with the specified ID from the database
                    Book bookToUpdate = bookRepository.GetBookByID(bookIDToUpdate);

                    if(bookToUpdate != null)
                    {
                        Console.WriteLine("Available Book Information:");
                        Console.WriteLine($"ID: {bookToUpdate.ID}, Book Name: {bookToUpdate.BookName}, Author: {bookToUpdate.Author}, Publisher: {bookToUpdate.Publisher}, ISBN: {bookToUpdate.ISBN}, Page Number: {bookToUpdate.PageNumber} ");

                        Console.WriteLine("Enter New Book Information: ");

                        Console.Write("Book Name: ");
                        string updatedBookName = Console.ReadLine();

                        Console.Write("Author: ");
                        string updatedAuthor = Console.ReadLine();

                        Console.Write("Publisher: ");
                        string updatedPublisher = Console.ReadLine();

                        Console.Write("ISBN: ");
                        string updatedISBN = Console.ReadLine();

                        Console.Write("Page Number: ");
                        int updatedPageNumber = int.Parse(Console.ReadLine());

                        // Update the book object with the new information
                        bookToUpdate.BookName = updatedBookName;
                        bookToUpdate.Author = updatedAuthor;
                        bookToUpdate.Publisher = updatedPublisher;
                        bookToUpdate.ISBN = updatedISBN;
                        bookToUpdate.PageNumber = updatedPageNumber;

                        // Update the book in the database
                        bookRepository.UpdateBook(bookToUpdate);

                        Console.WriteLine("The book has been successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine("No books with the specified ID were found.");
                    }
                    break;

                case '5':
                    Console.WriteLine("Exiting the programme...");
                    return;


                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }

            Console.WriteLine("Do you wish to continue? (E/H): ");
            response = Console.ReadKey().KeyChar;
            Console.WriteLine();

            

        }
        while (response == 'E' || response == 'e');
        
        Console.WriteLine("exiting the programme...");

        Console.ReadLine();

    }
}
