using System;
using System.Collections.Generic;

namespace FireDN.Data
{
    public partial class NguoiDung
    {
        public int Iduser { get; set; }
        public string Username { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string RoleUser { get; set; } = null!;
        public byte[]? HinhAnh { get; set; } 
        public string? Gmail { get; set; } 
    }
}
