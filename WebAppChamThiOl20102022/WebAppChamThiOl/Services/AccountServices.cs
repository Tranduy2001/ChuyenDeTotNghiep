using WebAppChamThiOl.Data;
using WebAppChamThiOl.Models;
using Microsoft.AspNetCore.Identity;
using static WebAppChamThiOl.Models.Constants;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppChamThiOl.Entities;
using Microsoft.Extensions.Hosting;
using static WebAppChamThiOl.Models.Constant;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WebAppChamThiOl.Services
{
    public class AccountServices
    {
        private readonly DataContext _context;
        public AccountServices(DataContext context)
        {
            _context = context;
        }

        public ResultStatus<LoginStatus, UserIdentity> Login(LoginViewModel login)
        {
            var user = _context.USERS.SingleOrDefault(x => x.UserName == login.UserName);
            if (user == null) return new ResultStatus<LoginStatus, UserIdentity>()
            {
                Code = LoginStatus.TaiKhoanKhongTonTai
            };

            var hasher = new PasswordHasher<USER>();
            var result = hasher.VerifyHashedPassword(user, user.Password, login.Password);
            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return new ResultStatus<LoginStatus, UserIdentity>()
                    {
                        Code = LoginStatus.DangNhapThatBai
                    };
                case PasswordVerificationResult.Success:
                    return new ResultStatus<LoginStatus, UserIdentity>()
                    {
                        Code = LoginStatus.DangNhapThanhCong,
                        Data = new UserIdentity()
                        {
                            UserName = user.UserName,
                            IsAdmin = user.IsAdmin,
                            FullName = user.FullName
                        }
                    };
                case PasswordVerificationResult.SuccessRehashNeeded:
                    break;
                default:
                    return new ResultStatus<LoginStatus, UserIdentity>()
                    {
                        Code = LoginStatus.DangNhapThatBai
                    };
            }
            return new ResultStatus<LoginStatus, UserIdentity>()
            {
                Code = LoginStatus.DangNhapThatBai
            };
        }

        public ResultStatus<RegisterStatus, UserIdentity> Register(RegisterViewModel model)
        {
            try
            {
                var user = _context.USERS.SingleOrDefault(x => x.UserName == model.UserName);
                if (user != null) return new ResultStatus<RegisterStatus, UserIdentity>()
                {
                    Code = RegisterStatus.TaiKhoanDaTonTai
                };
                var hasher = new PasswordHasher<USER>();
                var userModel = new USER() { FullName = model.FullName, UserName = model.UserName };
                var passHash = hasher.HashPassword(userModel, model.Password);
                userModel.Password = passHash;
                _context.USERS.Add(userModel);
                _context.SaveChanges();
                return new ResultStatus<RegisterStatus, UserIdentity>()
                {
                    Code = RegisterStatus.DangKyThanhCong,
                    Data = new UserIdentity()
                    {
                        FullName = userModel.FullName,
                        UserName = userModel.UserName,
                        IsAdmin = false
                    }
                };
            }
            catch (Exception)
            {
                return new ResultStatus<RegisterStatus, UserIdentity>()
                {
                    Code = RegisterStatus.DangKyThatBai
                };
            }
        }
        public List<USER> GetAll()
        {
            return _context.USERS.Where(x=>x.Role == (int) RoleTypeEnum.Student).ToList();
        }
        public async Task<PagingResult<USER>> GetAll(int page = 1, int pageSize = 12, string rankSort = null, string keyWord = null, bool isAdmin = false, int? role = null)
        {
            Expression<Func<USER, bool>> filter = null;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                filter = x => x.UserName.Contains(keyWord);
            }
            //if (!isAdmin)
            //{
            //    filter = x => x.IsAdmin == isAdmin;
            //}
            if (role.HasValue && role.Value != (int)RoleTypeEnum.Admin)
            {
                filter = x => x.Role == role.Value;
            }
            IQueryable<USER> data;

            if (filter != null)
            {
                data = _context.USERS.Where(filter).OrderByDescending(x => x.Id);
            }
            else
            {
                data = _context.USERS.OrderByDescending(x => x.Id);
            }
            return await PagingResult<USER>.CreateAsync(data, page, pageSize);
        }

        public async Task<USER> Get(int? id)
        {
            if (!id.HasValue) return null;
            var data = await _context.USERS.FindAsync(id);
            return data;
        }

        public void Add(UserCreateViewModel model)
        {
            var userModel = new USER() { FullName = model.FullName, UserName = model.UserName, IsAdmin = false, Role = model.Role  };
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var hasher = new PasswordHasher<USER>();
                var passHash = hasher.HashPassword(userModel, model.Password);
                userModel.Password = passHash;
            }
            _context.USERS.Add(userModel);
            _context.SaveChanges();
        }

        public void Update(UserUpdateViewModel model)
        {
            var user = _context.USERS.Where(x => x.UserName == model.UserName).FirstOrDefault();
            if (user != null)
            {
                _context.USERS.Attach(user);
                user.FullName = model.FullName;
                user.IsAdmin = model.IsAdmin;
                user.Role = model.Role;
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    var hasher = new PasswordHasher<USER>();
                    var userModel = new USER() { FullName = model.FullName, UserName = model.UserName };
                    var passHash = hasher.HashPassword(userModel, model.Password);
                    user.Password = passHash;
                }
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public USER GetById(int id)
        {
            return _context.USERS.FirstOrDefault(x => x.Id == id);
        }
        public UserUpdateViewModel GetByUserName(string userName)
        {
            var user = _context.USERS.FirstOrDefault(x => x.UserName == userName);
            if(user != null)
            return new UserUpdateViewModel()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                IsAdmin = user.IsAdmin,
                Password = user.Password,
                Role = user.Role
            };
            else
            {
                return null;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                USER user = _context.USERS.Find(id);
                _context.USERS.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
