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
    public class OrderDetailsController : ApiController
    {
        private masterEntities db = new masterEntities();


        // POST: api/OrderDetails
        [ResponseType(typeof(tbl_OrderDetails))]
        public IHttpActionResult Posttbl_OrderDetails(tbl_OrderDetails tbl_OrderDetails)
        {

            if (db.tbl_Products.Where(P => P.ProductId == tbl_OrderDetails.ProductId && P.Units == tbl_OrderDetails.Units).Count() == 0)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "this item does not exist"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Products = db.tbl_Products.Where(p => p.ProductId == tbl_OrderDetails.ProductId).First();
            Products.Units = Products.Units - tbl_OrderDetails.Units;

            db.tbl_OrderDetails.Add(tbl_OrderDetails);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_OrderDetailsExists(tbl_OrderDetails.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_OrderDetails.ProductId }, tbl_OrderDetails);
        }


        // GET: api/OrderDetails
        public IQueryable<tbl_OrderDetails> Gettbl_OrderDetails()
        {
            return db.tbl_OrderDetails;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(tbl_OrderDetails))]
        public IHttpActionResult Gettbl_OrderDetails(int id)
        {
            tbl_OrderDetails tbl_OrderDetails = db.tbl_OrderDetails.Find(id);
            if (tbl_OrderDetails == null)
            {
                return NotFound();
            }

            return Ok(tbl_OrderDetails);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_OrderDetails(int id, tbl_OrderDetails tbl_OrderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_OrderDetails.ProductId)
            {
                return BadRequest();
            }

            db.Entry(tbl_OrderDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_OrderDetailsExists(id))
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

       

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(tbl_OrderDetails))]
        public IHttpActionResult Deletetbl_OrderDetails(int id)
        {
            tbl_OrderDetails tbl_OrderDetails = db.tbl_OrderDetails.Find(id);
            if (tbl_OrderDetails == null)
            {
                return NotFound();
            }

            db.tbl_OrderDetails.Remove(tbl_OrderDetails);
            db.SaveChanges();

            return Ok(tbl_OrderDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_OrderDetailsExists(int id)
        {
            return db.tbl_OrderDetails.Count(e => e.ProductId == id) > 0;
        }
    }
}