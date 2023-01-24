using System;
using System.Collections.Generic;

#nullable disable

namespace MiamiHeatRest.Models
{
    public partial class ScoutingReport
    {
        public int PlayerKey { get; set; }
        public int TeamKey { get; set; }
        public int ScoutId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Comment { get; set; }
        public int Defense { get; set; }
        public int Rebound { get; set; }
        public int Shooting { get; set; }
        public int Assist { get; set; }
    }
}
