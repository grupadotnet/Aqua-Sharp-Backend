using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Config
    {
       
        public int ConfigId { get; set; }
        public string Password { get; set; } 
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool FirstRun { get; set; }
    }
}
