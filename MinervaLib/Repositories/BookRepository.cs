using MinervaLib.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaLib.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddBook(Book book)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Books (BookName, Author, Publisher, ISBN, PageNumber) " + "VALUES (@bookName, @author, @publisher, @isbn, @pageNumber)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookName", book.BookName);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@publisher", book.Publisher);
                    command.Parameters.AddWithValue("@isbn", book.ISBN);
                    command.Parameters.AddWithValue("@pageNumber", book.PageNumber);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBook(Book book) 
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Books SET BookName = @bookName, Author = @author, " + "Publisher = @publisher, ISBN = @isbn, PageNumber = @pageNumber " + "WHERE ID = @id";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookName", book.BookName);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@publisher", book.Publisher);
                    command.Parameters.AddWithValue("@isbn", book.ISBN);
                    command.Parameters.AddWithValue("@pageNumber", book.PageNumber);
                    command.Parameters.AddWithValue("@id", book.ID);

                    command.ExecuteNonQuery();
                }
            }

        }

        public void DeleteBook(int bookID)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Books WHERE ID = @id";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", bookID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books";

                using (OleDbCommand command = new OleDbCommand(@query, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = (int)reader["ID"];
                            string bookName = (string)reader["BookName"];
                            string author = (string)reader["Author"];
                            string publisher = (string)reader["Publisher"];
                            string isbn = (string)reader["ISBN"];
                            int pageNumber = (int)reader["PageNumber"];

                            Book book = new Book(id, bookName, author, publisher, isbn, pageNumber);
                            books.Add(book);

                        }
                    }
                }

            }
            return books;
        }

        public Book GetBookByID(int bookID)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books WHERE ID = @id";

                using (OleDbCommand command =  new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", bookID);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = (int)reader["ID"];
                            string bookName = (string)reader["BookName"];
                            string author = (string)reader["Author"];
                            string publisher = (string)reader["Publisher"];
                            string isbn = (string)reader["ISBN"];
                            int pageNumber = (int)reader["PageNumber"];

                            Book book = new Book(id, bookName, author, publisher, isbn, pageNumber);
                            return book;
                        }
                    }
                }
            }
            return null;
        }
    }
}
