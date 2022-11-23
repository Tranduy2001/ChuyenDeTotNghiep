using WebAppChamThiOl.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAppChamThiOl.Data
{
    public class DataContext: DbContext
    {
        public DataContext()
        {

        }
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<QUIZ> QUIZ { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<RESULT_QUIZ> RESULT_QUIZ { get; set; }
        public virtual DbSet<LOG_THI> LOG_THIS { get; set; }
        public DbSet<SUBJECT> SUBJECT { get; set; }
        public DbSet<LOG_CHAM_THI> LOG_CHAM_THIS { get; set; }
    }
}
