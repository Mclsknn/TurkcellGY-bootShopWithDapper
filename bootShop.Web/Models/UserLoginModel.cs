using System.ComponentModel.DataAnnotations;

namespace bootShop.Web.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Kullanıcı adı giriniz...")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre giriniz...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
