using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccDiscAPI.Models
{
    public class UserGuild
    {
        public long id;
        public string nick;

        public List<string> rolls;
        public string joined_at;

        //todo:
        //Method addroll
        //Method deleteroll
    }
}
