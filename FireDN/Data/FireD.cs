using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class FireD
    {
        public int Id { get; set; }
        public int? FireDId { get; set; }
        public string? Statistic { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
