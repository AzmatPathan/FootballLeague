using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballLeague.Models
{
    public class Football
    {
        [Key]
        public int clubID { get; set; }
        public string clubName { get; set; }

        public int goalsScored { get; set; }

        public int goalsConceeded { get; set; }

        public string Nationality { get; set; }

        public ICollection<Matches> matches { get; set; }


    }

    public class FootballDto
    {
        [Key]
        public int clubID { get; set; }
        public string clubName { get; set; }

        public int goalsScored { get; set; }

        public int goalsConceeded { get; set; }

        public string Nationality { get; set; }

    }
}