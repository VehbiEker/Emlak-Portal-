using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class emlakturuController : Controller
    {
        private readonly IemlakturuRepostory _emlakturuRepostory;

        public emlakturuController(IemlakturuRepostory context)
        {
            _emlakturuRepostory = context;
        }
        public IActionResult Index()
        {
            //_uygulamaDbContext.emlakturleri veriyi cekecek objemlakturuList ise veriyi alacak.

            List<emlakturu> objemlakturuList = _emlakturuRepostory.GetAll().ToList();

            // controllerin view kismina modelsi gonderebilmesi icin objemlakturulist i buraya yapistirdim.
            return View(objemlakturuList);
        }

        //emlak turu ekle butonu icin action olusturdum. sonrasinda emlak turu dosyasina razorview ekledim.
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(emlakturu emlakturu)
        {
            if (ModelState.IsValid)
            {
                _emlakturuRepostory.Ekle(emlakturu);
                _emlakturuRepostory.Kaydet(); //savechanges yapilmazsa bilgiler veri tabanina kaydedilmez.
                TempData["basarili"] = "Yeni kayıt başarıyla oluşturuldu!";
                return RedirectToAction("Index", "emlakturu"); //listeye gitmesi icin redirectToAction kullandim.
            }
            return View();
        }



        public IActionResult Duzenle(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            emlakturu? emlakturuvt = _emlakturuRepostory.Get(u=>u.Id==id); //kayitli olan id yi getirmek icin filtre
            if (emlakturuvt == null) 
            {
                return NotFound();
            }
            return View(emlakturuvt);
        }

        [HttpPost]
        public IActionResult Duzenle(emlakturu emlakturu)
        {
            if (ModelState.IsValid)
            {
                _emlakturuRepostory.Guncelle(emlakturu);
                _emlakturuRepostory.Kaydet(); //savechanges yapilmazsa bilgiler veri tabanina kaydedilmez.
                TempData["basarili"] = "Kayıt başarıyla düzenlendi!";
                return RedirectToAction("Index", "emlakturu"); //listeye gitmesi icin redirectToAction kullandim.
            }
            return View();
        }


        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            emlakturu? emlakturuvt = _emlakturuRepostory.Get(u => u.Id == id);
            if (emlakturuvt == null)
            {
                return NotFound();
            }
            return View(emlakturuvt);
        }


        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            emlakturu? emlakturu = _emlakturuRepostory.Get(u => u.Id == id);
            if (emlakturu == null)
            {
                return NotFound();
            }
            _emlakturuRepostory.Sil(emlakturu);
            _emlakturuRepostory.Kaydet();
            TempData["basarili"] = "Kayıt başarıyla silindi!";
            return RedirectToAction("Index", "emlakturu");

        }
    }
}
