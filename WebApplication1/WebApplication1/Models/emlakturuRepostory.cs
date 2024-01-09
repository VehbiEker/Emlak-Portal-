namespace WebApplication1.Models
{
    public class emlakturuRepostory : Repostory<emlakturu>, IemlakturuRepostory
    {
        private UygulamaDbContext _uygulamaDbContext;
        public emlakturuRepostory(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(emlakturu emlakturu)
        {
            _uygulamaDbContext.Update(emlakturu);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
