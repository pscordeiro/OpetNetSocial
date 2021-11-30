using System;

namespace OpetNet.Domain.Models
{
    public class PostsCurtidos
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public int PostId { get; set; }

    }
}
