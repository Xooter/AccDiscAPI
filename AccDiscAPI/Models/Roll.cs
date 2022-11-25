﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccDiscAPI.Models
{
    public class Roll
    {
        public long id;
        public string name;

        public int color;
        public string description;
        public int flags;
        public bool hoist;
        public bool managed;
        public bool mentionable;
        public int position;
        public long permissions;

        public int members;
    }
}
