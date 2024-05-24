using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class Temp
    {
        public int Id { get; set; }
        public int? TempId { get; set; }
        public string? Statistic { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
