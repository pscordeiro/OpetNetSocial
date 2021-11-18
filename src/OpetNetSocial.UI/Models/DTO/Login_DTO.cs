using System.ComponentModel.DataAnnotations;

namespace OpetNetSocial.UI.Models.DTO
{
    public class Login_DTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatorio")]
        [EmailAddress(ErrorMessage = "O formato do campo é de email")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "O campo precisar ter entre 5 e 200 caracteres")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatorio")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "O campo precisa ter entre 8 e 200 caracteres")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
