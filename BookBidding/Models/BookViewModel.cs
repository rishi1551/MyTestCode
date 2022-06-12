using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBidding.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
       

    }
}