namespace WebApplication1.Models
{
    public interface IEmlakRepostory : IRepostory<Emlak>
    {
        void Guncelle(Emlak Emlak);
        void Kaydet();
    }
}
