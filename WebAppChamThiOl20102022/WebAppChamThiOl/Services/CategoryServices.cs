using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Models;
using System.Linq.Expressions;
using static WebAppChamThiOl.Models.Constants;
using System.IO;
using WebAppChamThiOl.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAppChamThiOl.Services
{
    public class CategoryServices
    {
        private readonly DataContext dbContext;
        private QuizServices _quizServices;
        public CategoryServices(DataContext _dbContext, QuizServices quizServices)
        {
            dbContext = _dbContext;
            _quizServices = quizServices;
        }

        public List<SelectItem> GetAllSelect()
        {
            var data = dbContext.CATEGORY.Select(x => new SelectItem()
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();
            return data;
        }
        public async Task<PagingResult<CATEGORY>> GetAll(int page = 1, int pageSize = 12, string rankSort = null, string keyWord = null)
        {
            Expression<Func<CATEGORY, bool>> filter = null;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                filter = x => x.Name.Contains(keyWord);
            }
            IQueryable<CATEGORY> data;

            if (filter != null)
            {
                data = dbContext.CATEGORY.Include(x => x.SUBJECT).Where(filter).OrderByDescending(x => x.Id);
            }
            else
            {
                data = dbContext.CATEGORY.Include(x => x.SUBJECT).OrderByDescending(x => x.Id);
            }
            return await PagingResult<CATEGORY>.CreateAsync(data, page, pageSize);
        }
        public async Task<CATEGORY> Get(int? id)
        {
            if (!id.HasValue) return null;
            var data = await dbContext.CATEGORY.FindAsync(id);
            return data;
        }

        public void Add(CATEGORY model)
        {
            dbContext.CATEGORY.Add(model);
            dbContext.SaveChanges();
        }

        public void Update(CATEGORY model)
        {
            dbContext.Entry(model).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        public CATEGORY GetById(int id)
        {
            return dbContext.CATEGORY.Include(x => x.SUBJECT).FirstOrDefault(x => x.Id == id);
        }
        public bool Delete(int id)
        {
            try
            {
                CATEGORY dE = dbContext.CATEGORY.Find(id);
                dbContext.CATEGORY.Remove(dE);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LOG_THI GetDataThiSinhThi(string sbd)
        {
            try
            {
                return dbContext.LOG_THIS.Include(x => x.DE).Include(x => x.DE.QUIZS).Include(x => x.USER).FirstOrDefault(x => x.SBD == sbd);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public List<KetQuaChamThiViewModel> ChamThi(List<DataResultViewModel> dataResultViews)
        {
            List<KetQuaChamThiViewModel> ketQuas = new List<KetQuaChamThiViewModel>();
            foreach (var dataResultView in dataResultViews)
            {
                var deThi = GetDataThiSinhThi(dataResultView.SoBaoDanh);
                if (deThi != null)
                {
                    var cauHois = deThi.DE?.QUIZS?.ToList();
                    var soCauDung = 0;
                    var soCauHoi = cauHois.Count;
                    foreach (var item in dataResultView.KetQua)
                    {
                        var index = -1;
                        int.TryParse(item.Key, out index);
                        if (index < 0 || index > cauHois.Count)
                        {
                        }
                        else
                        {
                            var cauTl = cauHois?.OrderBy(x => x.Id).ToArray()[index - 1];
                            var cauTls = _quizServices.GetById(cauTl.Id)?.RESULT_QUIZS?.OrderBy(z => z.DisplayOrder).ToArray();
                            if (cauTls != null)
                            {
                                var indexDapAn = -1;
                                switch (item.Value.ToLower().ToString())
                                {
                                    case "a":
                                        indexDapAn = 0;
                                        break;
                                    case "b":
                                        indexDapAn = 1;
                                        break;
                                    case "c":
                                        indexDapAn = 2;
                                        break;
                                    case "d":
                                        indexDapAn = 3;
                                        break;
                                    default:
                                        break;
                                }
                                if (indexDapAn >= 0 && indexDapAn <= cauTls.Length && cauTls[indexDapAn].IsResultTrue)
                                {
                                    soCauDung++;
                                }
                            }
                        }

                    }
                    double diem = Math.Round((double)soCauDung / soCauHoi * 10, 2);

                    dbContext.LOG_CHAM_THIS.Add(new LOG_CHAM_THI()
                    {
                        SoCauTraLoiDung = soCauDung,
                        Diem = diem,
                        SBD = dataResultView.SoBaoDanh,
                        NgayChamThi = DateTime.Now,
                        SoCauHoi = soCauHoi,
                        TenThiSinh = deThi.USER?.FullName,
                        MaDe = deThi.DE?.Id.ToString()
                    });
                    dbContext.SaveChanges();

                    ketQuas.Add(new KetQuaChamThiViewModel()
                    {
                        Diem = diem,
                        SBD = dataResultView.SoBaoDanh,
                        SoCauTraLoiDung = soCauDung,
                        SoLuongCauHoi = soCauHoi
                    });
                }
            }

            return ketQuas;
        }
    }
}