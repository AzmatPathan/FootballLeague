using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballLeague.Models.ViewModels
{
    public class DetailsFootball
    {
        public FootballDto selectedFootball { get; set; }

        public IEnumerable<MatchesDto> ResponsibleMatches { get; set; }
    }
}