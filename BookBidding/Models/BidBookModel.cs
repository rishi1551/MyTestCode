using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBidding.Models
{
    public class BidBookModel
    {
        public int BidId { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
        public decimal BidAmount { get; set; }
    }
}