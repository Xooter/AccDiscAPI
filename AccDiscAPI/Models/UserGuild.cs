using System.Collections.Generic;

namespace AccDiscAPI.Models
{
    public class UserGuild
    {
        public long id { get; set; }
        public string nick { get; set; }

        public List<string> rolls { get; set; }
        public string joined_at { get; set; }

        //todo:
        //Method addroll
        //Method deleteroll
    }
}
