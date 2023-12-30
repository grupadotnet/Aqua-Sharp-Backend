using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        public int UsertId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int AuthId { get; set; }

        public Auth Auth { get; set; }

        public ICollection<Aquarium> Aquariums { get; set; }
    }
}
