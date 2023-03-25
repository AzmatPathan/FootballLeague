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
using FootballLeague.Migrations;
using FootballLeague.Models;

namespace FootballLeague.Controllers
{
    public class MatchesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MatchesData
        [HttpGet]
        public IEnumerable<MatchesDto> Listmatches()
        {
            List<Matches> matches = db.matches.ToList();
            List<MatchesDto> matchesDtos = new List<MatchesDto>();
            
            matches.ForEach(a => matchesDtos.Add(new MatchesDto()
            {
                matchID = a.matchID,
                homeClub = a.homeClub,
                awayClub = a.awayClub,
                score = a.score
            }));

            return matchesDtos;
        }


        [HttpGet]
        [ResponseType(typeof(MatchesDto))]
        public IEnumerable<MatchesDto> ListMatchesPlayed(int id)
        {
            List<Matches> matches = db.matches.Where(
                v => v.football.Any(
                    t => t.clubID == id)
                    ).ToList();
            List<MatchesDto> matchesDtos = new List<MatchesDto>();

            matches.ForEach(a => matchesDtos.Add(new MatchesDto()
            {
                matchID = a.matchID,
                homeClub = a.homeClub,
                awayClub = a.awayClub,
                score = a.score
            }));

            return matchesDtos;
        }



        // GET: api/MatchesData/5
        [HttpGet]
        [ResponseType(typeof(Matches))]
        public IHttpActionResult FindMatches(int id)
        {
            Matches matches = db.matches.Find(id);
            MatchesDto matchesDto = new MatchesDto()
            {
                matchID = matches.matchID,
                homeClub = matches.homeClub,
                awayClub = matches.awayClub,
                score = matches.score
            };
            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matchesDto);
        }

        // PUT: api/MatchesData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMatches(int id, Matches matches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matches.matchID)
            {
                return BadRequest();
            }

            db.Entry(matches).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchesExists(id))
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

        // POST: api/MatchesData
        [ResponseType(typeof(Matches))]
        [HttpPost]
        public IHttpActionResult AddMatches(Matches matches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.matches.Add(matches);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = matches.matchID }, matches);
        }

        // DELETE: api/MatchesData/5
        [ResponseType(typeof(Matches))]
        [HttpPost]
        public IHttpActionResult DeleteMatches(int id)
        {
            Matches matches = db.matches.Find(id);
            if (matches == null)
            {
                return NotFound();
            }

            db.matches.Remove(matches);
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

        private bool MatchesExists(int id)
        {
            return db.matches.Count(e => e.matchID == id) > 0;
        }
    }
}