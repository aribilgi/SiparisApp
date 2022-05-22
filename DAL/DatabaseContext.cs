using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        // .Net Core da Entity framework core mevcut fakat sql işlemleri için nugettan 4 farklı paket yüklemeliyiz bunlar; EntityFrameworkCore, EntityFrameworkCore.Desgin, EntityFrameworkCore.SqlServer, EntityFrameworkCore.Tools. Bunları yüklememiz gerekiyor
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // Kurucu metot içerisinde parametreyle yukardaki gibi DbContextOptions a DatabaseContext i options olarak gönderiyoruz
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        // override yazıp boşluk bırakıp on yazıp OnConfiguring metodunu mouse ile çift tıklayıp aşağıdaki metodu oluşturduk
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB; initial catalog=SiparisAlmaProgrami; integrated security=True; MultipleActiveResultSets=True;"); // Burada veritabanı olarak sql server kullanacağımızı belirtip parantez içinde connection string yani veritabanı bağlantı bilgilerimizi giriyoruz
        }
    }
}
