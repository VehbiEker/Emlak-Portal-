namespace WebApplication1.Models
{
    public class EmlakRepostory : Repostory<Emlak>, IEmlakRepostory
    {
        private UygulamaDbContext _uygulamaDbContext;
        public EmlakRepostory(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Emlak Emlak)
        {
            _uygulamaDbContext.Update(Emlak);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
