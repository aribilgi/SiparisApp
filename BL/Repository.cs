using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        DatabaseContext context;
        DbSet<T> _objectSet;
        public Repository()
        {
            if (context == null) // eğer context nullsa
            {
                context = new DatabaseContext(); // yeni context nesnesi oluştur
                _objectSet = context.Set<T>(); // _objectSet e de context hangi class için oluşturulmuşsa onu ata.
            }
        }
        public int Add(T entity)
        {
            _objectSet.Add(entity);
            return SaveChanges(); // geriye int olarak etkilenen kayıt sayısını döndük
        }

        public async Task AddAsync(T entity)
        {
            await _objectSet.AddAsync(entity); // asenkron metotlarda await anahtar kelimesi başa eklenir ve metot async ile asenkron hale getirilmelidir. await kısmına sağ tıklayıp make metot async ye tıklayarak metodu asenkron hale getirebiliriz.
        }

        public int Delete(T entity)
        {
            _objectSet.Remove(entity);
            return SaveChanges();
        }

        public T Find(int id)
        {
            return _objectSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _objectSet.FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _objectSet.FirstOrDefaultAsync(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _objectSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _objectSet.Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _objectSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _objectSet.Where(expression).ToListAsync();
        }

        public IQueryable<T> GetAllInclude(string table)
        {
            return _objectSet.Include(table); // EF Include metodu çalıştığı class ile parametreden gelen table isimli class ı birleştirip join yapar.
        }

        public int SaveChanges()
        {
            return SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }

        public int Update(T entity)
        {
            _objectSet.Update(entity);
            return SaveChanges();
        }
    }
}
