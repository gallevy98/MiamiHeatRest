using System;
using System.Collections.Generic;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class Team
    {
        public Team()
        {
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int TeamKey { get; set; }
        public int? LeagueKey { get; set; }
        public int? LeagueKeyDomestic { get; set; }
        public int? ArenaKey { get; set; }
        public string TeamName { get; set; }
        public string TeamNickname { get; set; }
        public string Conference { get; set; }
        public string SubConference { get; set; }
        public string TeamCity { get; set; }
        public string TeamCountry { get; set; }
        public string CoachName { get; set; }
        public string Urlphoto { get; set; }
        public bool? CurrentNbateamFlg { get; set; }

        public virtual League LeagueKeyDomesticNavigation { get; set; }
        public virtual League LeagueKeyNavigation { get; set; }
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
