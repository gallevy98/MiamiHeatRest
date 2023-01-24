using System;
using System.Collections.Generic;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class TeamPlayer
    {
        public int PlayerKey { get; set; }
        public int TeamKey { get; set; }
        public int SeasonKey { get; set; }
        public bool? ActiveTeamFlg { get; set; }
        public DateTime DwhInsertDatetime { get; set; }

        public virtual Player PlayerKeyNavigation { get; set; }
        public virtual Team TeamKeyNavigation { get; set; }
    }
}
