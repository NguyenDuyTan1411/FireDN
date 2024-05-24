using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class Smoke
    {
        public int Id { get; set; }
        public int? SmokeId { get; set; }
        public string? Statistic { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
