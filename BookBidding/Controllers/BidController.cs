using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookBidding.Models;
using System.Data.Entity;
namespace BookBidding.Controllers
{
    public class BidController : ApiController
    {
        //Get- retrive data
        public IHttpActionResult GetAllBids()
        {
            IList<BidBookModel> bids = null;
            using (var x = new BiddingDatabaseEntities1())
            {
                bids = x.BookBids
                    .Select(c => new BidBookModel()
                    {
                        BidId=c.BidID,
                        BookName = c.BookName,
                        UserName = c.UserName,
                        BidAmount = (decimal)(double)c.BidAmount

                    }).ToList<BidBookModel>();

            }
            if (bids.Count == 0)
            {
                return NotFound();
            }
            return Ok();

        }

        //Post- insert new record

        public IHttpActionResult PostNewBid(BidBookModel bid)
        {
            if (!ModelState.IsValid)

                return BadRequest("Invalid data. Please check.");
            using (var x = new BiddingDatabaseEntities1())
            {
                x.BookBids.Add(new BookBid()               
                {
                    BidID = bid.BidId,
                    BookName = bid.BookName,
                    UserName = bid.UserName,
                    BidAmount = (decimal?)(double)bid.BidAmount

                });
                x.SaveChanges();
            }
            return Ok();

        }

        //Put- update data base on id

        public IHttpActionResult PutBid(BidBookModel bid)
        {
            if (!ModelState.IsValid)

                return BadRequest("This is invalid model. Please recheck.");
            using (var x = new BiddingDatabaseEntities1())
            {
                var checkExistingBooks = x.BookBids.Where(c => c.BidID == bid.BidId).FirstOrDefault();

                if (checkExistingBooks != null)
                {
                    checkExistingBooks.BookName = bid.BookName;
                    checkExistingBooks.UserName = bid.UserName;
                    checkExistingBooks.BidAmount = (decimal?)(double?)bid.BidAmount;
                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
    }
}
