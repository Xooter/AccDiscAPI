using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccDiscAPI.Models
{
    public class Invite
    {
        public string code;
        public int uses;
        public string expires_at;
        public string created_at;
        public User inviter;
        public bool temporary;
        public int max_age;
        public int max_uses;
    }
}
