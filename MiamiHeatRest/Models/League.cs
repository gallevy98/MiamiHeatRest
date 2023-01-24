using System;
using System.Collections.Generic;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class League
    {
        public League()
        {
            TeamLeagueKeyDomesticNavigations = new HashSet<Team>();
            TeamLeagueKeyNavigations = new HashSet<Team>();
        }

        public int LeagueKey { get; set; }
        public string LeagueName { get; set; }
        public string Country { get; set; }
        public bool? ActiveSource { get; set; }
        public int? LeagueGroupKey { get; set; }
        public int? LeagueCustomGroupKey { get; set; }
        public bool? SearchDisplayFlag { get; set; }

        public virtual ICollection<Team> TeamLeagueKeyDomesticNavigations { get; set; }
        public virtual ICollection<Team> TeamLeagueKeyNavigations { get; set; }
    }
}
