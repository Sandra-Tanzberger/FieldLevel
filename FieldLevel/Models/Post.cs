﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldLevel.Models
{
    internal class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}