using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballLeague.Models
{
    public class Matches
    {
        [Key]
        public int matchID { get; set; }

        public string homeClub { get; set; }
        
        public string awayClub { get; set; }

        public string score { get; set; }

        public ICollection<Football> football { get; set; }

    }

    public class MatchesDto
    {
        [Key]
        public int matchID { get; set; }

        public string homeClub { get; set; }

        public string awayClub { get; set; }

        public string score { get; set; }

    }
}