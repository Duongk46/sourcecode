using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class LoginModel
    {
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string Username { set; get; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}
