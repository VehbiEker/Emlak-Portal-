using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Repostory<T> : IRepostory<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbset;

        public Repostory(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
            this.dbset = _uygulamaDbContext.Set<T>();
            _uygulamaDbContext.Emlaklar.Include(x => x.emlakturu);
        }
        public void Ekle(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filtre, string? includeProps = null) //cslismazsa stem.Linq.Expressions kismini sil
        {
            IQueryable<T> sorgu = dbset;
            sorgu = sorgu.Where(filtre);

            if (!string.IsNullOrEmpty(includeProps)) // foreach dongusu 
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }

            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> sorgu = dbset;

            if(!string.IsNullOrEmpty(includeProps)) // foreach dongusu 
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);  
                }
            }

            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbset.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }
    }
}
