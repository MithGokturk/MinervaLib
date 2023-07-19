using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinervaLib.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public int PageNumber { get; set; }

        //Construction Method
        public Book(int id, string bookName, string author, string publisher, string isbn, int pageNumber)
        {
            ID = id;
            BookName = bookName;
            Author = author;
            Publisher = publisher;
            ISBN = isbn;
            PageNumber = pageNumber;
        }

    }


}
