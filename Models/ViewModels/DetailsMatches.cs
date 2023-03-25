using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballLeague.Models.ViewModels
{
    public class DetailsMatches
    {
        public MatchesDto selectedMatches { get; set; }

        public IEnumerable<FootballDto> ResponsibleFootball { get; set; }
    }
}