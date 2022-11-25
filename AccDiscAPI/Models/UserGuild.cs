using System.Collections.Generic;

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
