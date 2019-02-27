using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_2
{
   public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public Book (string title, string author, string genre)
        {
            Title = title;
            Author = author;
            Genre = genre;
          
        }

        public Book()
        {
        }

        public override string ToString()
        {
            return $"{Title} by {Author} Genre: {Genre}";
        }
    }
}
