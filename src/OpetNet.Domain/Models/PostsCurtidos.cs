﻿using System;
using System.Collections.Generic;

namespace OpetNet.Domain.Models
{
    public class PostsCurtidos
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
