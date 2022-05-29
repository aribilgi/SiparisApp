using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BL
{
    public interface IRepository<T> where T : class // Buraya gönderilecek T bir class olmalıdır!
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        int Add(T entity);
        T Find(int id);
        T Get(Expression<Func<T, bool>> expression);
        int Update(T entity);
        int Delete(T entity);
        int SaveChanges();
        IQueryable<T> GetAllInclude(string table); // Entity framework de include yöntemiyle bir biri ile bağlantılı class lar ı sql deki join işlemiyle birleştirebiliyoruz.
        // Asenkron Metotlar : Uygulamada işlemlerin bir birini beklemesine gerek kalmadan her işlemin ayrı ayrı yürütülmesini asenkron metotlar ile yapabiliyoruz. Böylece uygulamalardaki kilitlenme sorunlarını da minimuma indirmiş oluyoruz.
        Task<T> FindAsync(int id); // asenkron metotlar Task ile tanımlanır. FindAsync metot isminin sonuna async eklenerek bu metodun asenkron çalışacağını anlamamızı sağlar.
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); // Asenkron olarak void yani geri değer döndürmeyen metot bu şekilde oluşturuluyor.
        Task<int> SaveChangesAsync();
        // Not : Asenkron metotlarda update ve delete için asenkron metot yoktur.
    }
}
