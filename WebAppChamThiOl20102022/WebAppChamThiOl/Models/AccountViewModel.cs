using System.ComponentModel.DataAnnotations;

namespace WebAppChamThiOl.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [Display(Name = "Tài khoản")]
        //[UserNameAddress]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống!")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Trường này không được để trống!")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Vai trò tài khoản")]
        public int Role { get; set; }
    }
    public class UserUpdateViewModel
    {
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Trường này không được để trống!")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [Display(Name = "Họ tên")]
        public string? FullName { get; set; }
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "Là tài khoản quản trị")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Vai trò tài khoản")]
        public int Role { get; set; }
    }

    public class UserCreateViewModel
    {
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Trường này không được để trống!")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [Display(Name = "Họ tên")]
        public string? FullName { get; set; }

        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string? ConfirmPassword { get; set; }
        //[Display(Name = "Là tài khoản quản trị")]
        //public bool IsAdmin { get; set; }
        [Display(Name = "Vai trò tài khoản")]
        [Required(ErrorMessage = "Trường này không được để trống!")]
        public int Role { get; set; }
    }
}
