using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.Shared.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
