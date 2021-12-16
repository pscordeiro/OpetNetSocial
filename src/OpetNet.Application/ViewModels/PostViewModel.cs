using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpetNet.Application.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [StringLength(1000, MinimumLength = 2)]
        public string Mensagem { get; set; }
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool Liked { get; set; } 

        public virtual CustomerViewModel Customer { get; set; }
        public virtual ICollection<PostCurtidosViewlModel> PostCurtidos { get; set; }
    }
}
