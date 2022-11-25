﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccDiscAPI.Models
{
    public class Attachment
    {
        public long id;
        public string content_type;
        public string filename;
        public int height;
        public int width;
        public string proxy_url;
        public int size;
        public string url;
    }
}