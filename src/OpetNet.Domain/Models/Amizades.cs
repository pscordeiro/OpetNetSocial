using System.ComponentModel.DataAnnotations;
using System;

namespace OpetNet.Domain.Models
{
    public class Amizades
    {
        [Key]
        public int IdAmizade { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdAmigo { get; set; }
    }
}
