using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class TgSensor
    {
        public int? TgSid { get; set; }
        public int? EspId { get; set; }

        public virtual Esp? Esp { get; set; }
        public virtual Sensor? TgS { get; set; }
    }
}
