using System;
using System.Collections.Generic;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class Player
    {
        public Player()
        {
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int PlayerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? PositionKey { get; set; }
        public int? AgentKey { get; set; }
        public int? FreeAgentYear { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int? YearsOfService { get; set; }
        public decimal? Wing { get; set; }
        public decimal? BodyFat { get; set; }
        public decimal? StandingReach { get; set; }
        public decimal? CourtRunTime34 { get; set; }
        public decimal? VerticalJumpNoStep { get; set; }
        public decimal? VerticalJumpMax { get; set; }
        public decimal? HandWidth { get; set; }
        public decimal? HandLength { get; set; }
        public string Urlphoto { get; set; }
        public bool ActiveAnalysisFlg { get; set; }
        public int? LeagueCustomGroupKey { get; set; }
        public int? BboPlayerKey { get; set; }
        public DateTime DwhInsertDatetime { get; set; }
        public DateTime? DwhUpdateDatetime { get; set; }
        public string AgentName { get; set; }
        public string AgentPhone { get; set; }
        public string CommittedTo { get; set; }
        public string Handedness { get; set; }
        public int? GlplayerKey { get; set; }
        public int? PlayerStatusKey { get; set; }
        public string HeightSource { get; set; }
        public string WeightSource { get; set; }
        public string WingSource { get; set; }
        public string BodyFatSource { get; set; }
        public string StandingReachSource { get; set; }
        public string CourtRunTime34Source { get; set; }
        public string VerticalJumpNoStepSource { get; set; }
        public string VerticalJumpMaxSource { get; set; }
        public string HandWHSource { get; set; }
        public string Hand { get; set; }
        public bool IsCustomData { get; set; }
        public string HandednessSource { get; set; }

        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
