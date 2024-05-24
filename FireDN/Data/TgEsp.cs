using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class TgEsp
    {
        public int? EspId { get; set; }
        public int? FireDId { get; set; }
        public int? HumiId { get; set; }
        public int? TempId { get; set; }
        public int? SmokeId { get; set; }
        public DateTime? Record { get; set; }
        public string? Alerts { get; set; }

        public virtual Esp? Esp { get; set; }
        public virtual FireD? FireD { get; set; }
        public virtual Humi? Humi { get; set; }
        public virtual Smoke? Smoke { get; set; }
        public virtual Temp? Temp { get; set; }
    }
}
