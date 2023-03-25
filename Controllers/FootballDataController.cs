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
using FootballLeague.Models;

namespace FootballLeague.Controllers
{
    public class FootballDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FootballData
        [HttpGet]
        public IEnumerable<FootballDto> Listfootballs()
        {
            List<Football> footballs=  db.footballs.ToList();
            List<FootballDto> footballDtos = new List<FootballDto>();

            footballs.ForEach(a => footballDtos.Add(new FootballDto()
            {
                clubID = a.clubID,
                clubName = a.clubName,
                goalsScored = a.goalsScored,
                goalsConceeded = a.goalsConceeded,
                Nationality = a.Nationality,
            }));

            return footballDtos;

        }


        [HttpGet]
        [ResponseType(typeof(FootballDto))]
        public IEnumerable<FootballDto> ListFootballForMatches(int id)
        {
            List<Football> football = db.footballs.Where(
                v => v.matches.Any(
                    t => t.matchID == id)
                    ).ToList();
            List<FootballDto> footballDtos = new List<FootballDto>();

            football.ForEach(a => footballDtos.Add(new FootballDto()
            {
                clubID = a.clubID,
                clubName = a.clubName,
                goalsScored = a.goalsScored,
                goalsConceeded = a.goalsConceeded,
                Nationality = a.Nationality,
            }));

            return footballDtos;
        }





        // GET: api/FootballData/5
        [ResponseType(typeof(Football))]
        [HttpGet]
        public IHttpActionResult FindFootball(int id)
        {
            Football football = db.footballs.Find(id);
            FootballDto footballDto = new FootballDto()
            {
                clubID = football.clubID,
                clubName = football.clubName,
                goalsScored = football.goalsScored,
                goalsConceeded = football.goalsConceeded,
                Nationality = football.Nationality,
            };

            if (football == null)
            {
                return NotFound();
            }

            return Ok(footballDto);
        }

        // PUT: api/FootballData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFootball(int id, Football football)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != football.clubID)
            {
                return BadRequest();
            }

            db.Entry(football).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootballExists(id))
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

        // POST: api/FootballData
        [ResponseType(typeof(Football))]
        [HttpPost]
        public IHttpActionResult AddFootball(Football football)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.footballs.Add(football);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = football.clubID }, football);
        }

        // DELETE: api/FootballData/5
        [ResponseType(typeof(Football))]
        [HttpPost]
        public IHttpActionResult DeleteFootball(int id)
        {
            Football football = db.footballs.Find(id);
            if (football == null)
            {
                return NotFound();
            }

            db.footballs.Remove(football);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FootballExists(int id)
        {
            return db.footballs.Count(e => e.clubID == id) > 0;
        }
    }
}