using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebAppChamThiOl.Models.Constant;

namespace WebAppChamThiOl.Data
{
    [Table("User")]
    public class USER
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tài khoản")]
        public string? UserName { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]

        public string? Password { get; set; }
        [Required]
        [Display(Name = "Họ và tên")]

        public string? FullName { get; set; }
        [Display(Name = "Là tài khoản quản trị")]

        public bool IsAdmin { get; set; }
        [Display(Name = "Vai trò tài khoản")]
        public int Role { get; set; }
        [NotMapped]
        public string RoleName
        {
            get
            {
                switch (Role)
                {
                    case (int)RoleTypeEnum.Admin:
                        {
                            return "Quản trị viên";
                        }
                    case (int)RoleTypeEnum.User:
                        {
                            return "Người dùng";

                        }
                    case (int)RoleTypeEnum.Student:
                        {
                            return "Thí sinh";
                        }
                    default:
                        return "";
                }
            }
        }
    }
}
