namespace WebApplication1.Models
{
    public interface IemlakturuRepostory : IRepostory<emlakturu>
    {
        void Guncelle(emlakturu emlakturu);
        void Kaydet();
    }
}
