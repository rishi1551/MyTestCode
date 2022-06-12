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
    public class SoldController : ApiController
    {
        //Get- retrive data
        public IHttpActionResult GetAllSold()
        {
            IList<BookSoldModel> sold = null;
            using (var x = new BiddingDatabaseEntities2())
            {
                sold = x.BookSolds
                    .Select(c => new BookSoldModel()
                    {
                        SaleId=c.SaleId,
                        BookName = c.BookName,
                        UserName = c.UserName,
                        BookPrice = (decimal)(double)c.BookPrice,
                        SalePrice = (decimal)(double)c.SalePrice

                    }).ToList<BookSoldModel>();

            }
            if (sold.Count == 0)
            {
                return NotFound();
            }
            return Ok();

        }

        //Post- insert new record

        public IHttpActionResult PostNewSale(BookSoldModel sold)
        {
            if (!ModelState.IsValid)

                return BadRequest("Invalid data. Please check.");
            using (var x = new BiddingDatabaseEntities2())
            {
                x.BookSolds.Add(new BookSold()
                {
                    SaleId=sold.SaleId,
                    BookName = sold.BookName,
                    UserName = sold.UserName,
                    BookPrice = (decimal?)(double)sold.BookPrice,
                    SalePrice = (decimal?)(double)sold.SalePrice

                });
                x.SaveChanges();
            }
            return Ok();

        }

        //Put- update data base on id

        public IHttpActionResult PutSale(BookSoldModel sold)
        {
            if (!ModelState.IsValid)

                return BadRequest("This is invalid model. Please recheck.");
            using (var x = new BiddingDatabaseEntities2())
            {
                var checkExistingBooks = x.BookSolds.Where(c => c.SaleId == sold.SaleId).FirstOrDefault();

                if (checkExistingBooks != null)
                {
                    checkExistingBooks.BookName = sold.BookName;
                    checkExistingBooks.UserName = sold.UserName;
                    checkExistingBooks.BookPrice = (decimal?)(double?)sold.BookPrice;
                    checkExistingBooks.SalePrice = (decimal?)(double?)sold.SalePrice;
                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
    }
}
