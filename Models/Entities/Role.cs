using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public enum RoleName
    { 
      Own=1,
      All=8
    }
    public class Role
    {
        public int Id { get; set; }
        public RoleName Name { get; set; }
    }
}
