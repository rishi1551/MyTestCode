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
    public class BookController : ApiController
    {
        //Get- retrive data
        public IHttpActionResult GetAllBooks()
        {
            IList<BookViewModel> books = null;
            using (var x = new BiddingDatabaseEntities())
            {
                books = x.BookDetails
                    .Select(c => new BookViewModel()
                    {
                        BookId = c.BookId,
                        BookName = c.BookName,
                        Author = c.Author,
                        Price = (double)c.Price
                        
                    }).ToList<BookViewModel>();

            }
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok();

        }

        //Post- insert new record

        public IHttpActionResult PostNewBook(BookViewModel book)
        {
            if(!ModelState.IsValid)
            
                return BadRequest("Invalid data. Please check.");
            using (var x = new BiddingDatabaseEntities())
            {
                x.BookDetails.Add(new BookDetail()
                {
                    BookId = book.BookId,
                    BookName=book.BookName,
                    Author=book.Author,
                    Price= (decimal?)(double)book.Price
                   
                }) ;
                x.SaveChanges();
            }
            return Ok();

        }
        //Put- update data base on id

        public IHttpActionResult PutBook(BookViewModel book)
        {
            if (!ModelState.IsValid)

                return BadRequest("This is invalid model. Please recheck.");
            using (var x = new BiddingDatabaseEntities())
            {
                var checkExistingBooks = x.BookDetails.Where(c => c.BookId == book.BookId)
                                                            .FirstOrDefault<BookDetail>();
                if (checkExistingBooks != null)
                {
                    checkExistingBooks.BookName = book.BookName;
                    checkExistingBooks.Author = book.Author;
                    checkExistingBooks.Price = (decimal?)(double?)book.Price;
                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
        //Delete- delete data base on id
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please enter valid book id");
            using (var x = new BiddingDatabaseEntities())
            {
                var book = x.BookDetails.Where(c => c.BookId == id).FirstOrDefault();
                x.Entry(book).State = System.Data.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }
    }
}
