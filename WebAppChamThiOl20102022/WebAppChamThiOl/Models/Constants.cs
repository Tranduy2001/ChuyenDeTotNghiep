using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebAppChamThiOl.Models
{
    public static class Constants
    {
        public const string UserIdentity = "UserIdentity";
        public enum LoginStatus
        {
            [Display(Name = "Đăng nhập thành công")]
            DangNhapThanhCong = 0,
            [Display(Name = "Đăng nhập thất bại")]
            DangNhapThatBai,
            [Display(Name = "Sai mật khẩu hoặc tài khoản")]
            SaiMatKhauHoacTaiKhoan,
            [Display(Name = "Tài khoản không tồn tại")]
            TaiKhoanKhongTonTai
        }

        public enum RegisterStatus
        {
            [Display(Name = "Đăng ký thành công")]
            DangKyThanhCong = 0,
            [Display(Name = "Đăng ký thất bại")]
            DangKyThatBai,
            [Display(Name = "Tài khoản đã tồn tại")]
            TaiKhoanDaTonTai
        }

        public enum StatusEnum
        {
            [Display(Name = "Đạt")]
            Dat,
            [Display(Name = "Trượt")]

            Truot,
            [Display(Name = "Thi lại")]

            ThiLai
        }


        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class ResultStatus<T, M>
    {
        public T Code { get; set; }
        public M? Data { get; set; }
    }
    public class SelectItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public enum QuizTypeEnum
    {
        [Display(Name = "Câu hỏi hình ảnh")]
        QuizPicture = 1,
        [Display(Name = "Câu hỏi kiểm tra")]
        QuizTest
    }
}
