using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpetNet.Application.ViewModels
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The PassWord is Required")]
        [DisplayName("Senha")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inv?lido")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        public string UrlImgProfile { get; set; }
        public string UrlProfile { get; set; }
    }
}
