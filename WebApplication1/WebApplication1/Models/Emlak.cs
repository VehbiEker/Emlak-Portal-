using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Emlak
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string EmlakAdi { get; set; }

        public string Aciklama { get; set;}
        
        [Required]
        public string Il { get; set; }

        [Required]
        public string Ilce { get; set; }

        [Required]
        public string Mahalle { get; set; }

        [Required]
        public string Fiyat { get; set;}

        [Required]
        public string Durumu { get; set; }

        [ValidateNever]
        public int emlakturuId { get; set; }
        [ForeignKey("emlakturuId")]  
        
        [ValidateNever]
        public emlakturu emlakturu { get; set; }

        [ValidateNever]
        public string ResimUrl { get; set; }



    }
}
