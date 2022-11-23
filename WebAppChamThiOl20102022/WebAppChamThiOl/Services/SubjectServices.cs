using WebAppChamThiOl.Data;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAppChamThiOl.Services
{
    public class SubjectServices
    {
        private readonly DataContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public SubjectServices(DataContext context)
        {
            _context = context;
        }
        public async Task<List<SUBJECT>> GetAll(int page = 1, int pageSize = 10, string keyWord = null/*, int? deId = null*/)
        {
            IQueryable<SUBJECT> data = _context.SUBJECT.OrderByDescending(x => x.Id);
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.Where(x => x.Name.Trim().Contains(keyWord.Trim()));
            }
            return await PagingResult<SUBJECT>.CreateAsync(data, page, pageSize);
        }
        public void Add(SUBJECT model)
        {
            _context.SUBJECT.Add(model);
            _context.SaveChanges();
        }

        public void Update(SUBJECT model)
        {

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public SUBJECT GetById(int id)
        {
            return _context.SUBJECT.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                SUBJECT dE = _context.SUBJECT.Find(id);
                _context.SUBJECT.Remove(dE);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<SelectItem> GetAll()
        {
            return _context.SUBJECT.Select(x => new SelectItem() { Text = x.Name, Value = x.Id }).ToList();
        }
    }
}
