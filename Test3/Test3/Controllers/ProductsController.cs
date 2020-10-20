using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Test3.Models;

namespace Test3.Controllers
{
    public class ProductsController : ApiController
    {
        private masterEntities db = new masterEntities();

        // GET: api/Products
        public IQueryable<tbl_Products> Gettbl_Products()
        {
            return db.tbl_Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(tbl_Products))]
        public IHttpActionResult Gettbl_Products(int id)
        {
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return NotFound();
            }

            return Ok(tbl_Products);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Products(int id, tbl_Products tbl_Products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Products.ProductId)
            {
                return BadRequest();
            }

            db.Entry(tbl_Products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(tbl_Products))]
        public IHttpActionResult Posttbl_Products(tbl_Products tbl_Products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Products.Add(tbl_Products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_ProductsExists(tbl_Products.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Products.ProductId }, tbl_Products);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(tbl_Products))]
        public IHttpActionResult Deletetbl_Products(int id)
        {
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return NotFound();
            }

            db.tbl_Products.Remove(tbl_Products);
            db.SaveChanges();

            return Ok(tbl_Products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_ProductsExists(int id)
        {
            return db.tbl_Products.Count(e => e.ProductId == id) > 0;
        }
    }
}