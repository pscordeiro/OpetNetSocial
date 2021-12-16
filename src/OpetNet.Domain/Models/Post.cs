using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpetNet.Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<PostsCurtidos> PostsCurtidos { get; set; }
    }
}
