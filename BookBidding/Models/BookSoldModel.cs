using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBidding.Models
{
    public class BookSoldModel
    {
        public int SaleId { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
        public decimal BookPrice { get; set; }
        public decimal SalePrice { get; set; }
    }
}