using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class emlakturu
    {
        [Key] //primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen Gerekli Alanları Doldurun!")] //bos kayit girilmemesi icin.
        [MaxLength(40)] //veritabanina girilebilecek max karakter sayisi.
        [DisplayName("Emlak Türü Adı")] //Turu yazildiginda ekrana bunu yansitiyor.
        public string Turu { get; set; }
    }
}
