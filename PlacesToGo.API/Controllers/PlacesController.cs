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
using PlacesToGo.API.Models;
using System.Data.Entity.Spatial;

namespace PlacesToGo.API.Controllers
{
    public class PlacesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Places
        public IQueryable<Places> GetPlaces()
        {
            return db.Places;
        }

        // GET: api/Places/5
        //[ResponseType(typeof(Places))]
        //public IHttpActionResult GetPlaces(int id)
        //{
        //    Places places = db.Places.Find(id);
        //    if (places == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(places);
        //}

        // PUT: api/Places/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlaces(int id, Places places)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != places.PlacesId)
            {
                return BadRequest();
            }

            db.Entry(places).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacesExists(id))
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

        // POST: api/Places
        [ResponseType(typeof(Places))]
        public IHttpActionResult PostPlaces(Places places)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Places.Add(places);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = places.PlacesId }, places);
        }

        // DELETE: api/Places/5
        [ResponseType(typeof(Places))]
        public IHttpActionResult DeletePlaces(int id)
        {
            Places places = db.Places.Find(id);
            if (places == null)
            {
                return NotFound();
            }

            db.Places.Remove(places);
            db.SaveChanges();

            return Ok(places);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Places GetPlaces(double longitude, double latitude)
        {
            var location = DbGeography.FromText(
                string.Format("POINT ({0} {1})", longitude, latitude));

            var query = from a in db.Places
                        orderby a.Location.Distance(location)
                        select a;

            return query.FirstOrDefault();
        }

        private bool PlacesExists(int id)
        {
            return db.Places.Count(e => e.PlacesId == id) > 0;
        }
    }
}