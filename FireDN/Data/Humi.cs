using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class Humi
    {
        public int Id { get; set; }
        public int? HumiId { get; set; }
        public string? Statistic { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
