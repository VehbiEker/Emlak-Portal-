using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{

    public class EmlakController : Controller
    {
        private readonly IEmlakRepostory _EmlakRepostory;
        private readonly IemlakturuRepostory _emlakturuRepostory;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public EmlakController(IEmlakRepostory EmlakRepostory, IemlakturuRepostory emlakturuRepostory, IWebHostEnvironment webHostEnvironment)
        {
            _EmlakRepostory = EmlakRepostory;
            _emlakturuRepostory = emlakturuRepostory;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin,Kullanici")]

        public IActionResult Index()
        {
            //_uygulamaDbContext.emlak veriyi cekecek objemlakList ise veriyi alacak.

            // List<Emlak> objEmlakList = _EmlakRepostory.GetAll().ToList();
            List<Emlak> objEmlakList = _EmlakRepostory.GetAll(includeProps:"emlakturu").ToList();


            // controllerin view kismina modelsi gonderebilmesi icin objemlaklist i buraya yapistirdim.
            return View(objEmlakList);
        }

        //emlak turu ekle butonu icin action olusturdum. sonrasinda emlak  dosyasina razorview ekledim.

        [Authorize(Roles = UserRoles.Role_Admin)]

        public IActionResult EkleDuzenle(int? id)
        {
            IEnumerable<SelectListItem> emlakturuList = _emlakturuRepostory.GetAll()
                .Select(e => new SelectListItem
                {
                    Text = e.Turu,
                    Value = e.Id.ToString() //
                });

            ViewBag.emlakturuList = emlakturuList;

            if (id == null || id == 0)
            {//ekleme
                return View();
            }
            else
            {
                //Duzenleme
                Emlak? Emlakvt = _EmlakRepostory.Get(u => u.Id == id); //kayitli olan id yi getirmek icin filtre
                if (Emlakvt == null)
                {
                    return NotFound();
                }
                return View(Emlakvt);
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]

        public IActionResult EkleDuzenle(Emlak Emlak,IFormFile? file)
        {
            //var errors = ModelState.Values.SelectMany(x => x.Errors); 
            //debug koyduktsn sonra f10a bas hatalari gosterir 

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string EmlakPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(EmlakPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    Emlak.ResimUrl = @"\img\" + file.FileName;
                }

                if (Emlak.Id == 0 )
                {
                    _EmlakRepostory.Ekle(Emlak);
                    TempData["basarili"] = "Yeni kayıt başarıyla oluşturuldu!";
                }
                else
                {
                    _EmlakRepostory.Guncelle(Emlak);
                    TempData["basarili"] = "Duzenleme başarıyla tamalandı!";
                }
                _EmlakRepostory.Kaydet(); //savechanges yapilmazsa bilgiler veri tabanina kaydedilmez.
                return RedirectToAction("Index", "Emlak"); //listeye gitmesi icin redirectToAction kullandim.
            }
            return View();
        }


        /*
        public IActionResult Duzenle(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            Emlak? Emlakvt = _EmlakRepostory.Get(u=>u.Id==id); //kayitli olan id yi getirmek icin filtre
            if (Emlakvt == null) 
            {
                return NotFound();
            }
            return View(Emlakvt);
        }
        */
        /* [HttpPost]
         public IActionResult Duzenle(Emlak Emlak)
         {
             if (ModelState.IsValid)
             {
                 _EmlakRepostory.Guncelle(Emlak);
                 _EmlakRepostory.Kaydet(); //savechanges yapilmazsa bilgiler veri tabanina kaydedilmez.
                 TempData["basarili"] = "Kayıt başarıyla düzenlendi!";
                 return RedirectToAction("Index", "Emlak"); //listeye gitmesi icin redirectToAction kullandim.
             }
             return View();
         }
        */

        [Authorize(Roles = UserRoles.Role_Admin)]

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Emlak? Emlakvt = _EmlakRepostory.Get(u => u.Id == id);
            if (Emlakvt == null)
            {
                return NotFound();
            }
            return View(Emlakvt);
        }


        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]

        public IActionResult SilPOST(int? id)
        {
            Emlak? Emlak = _EmlakRepostory.Get(u => u.Id == id);
            if (Emlak == null)
            {
                return NotFound();
            }
            _EmlakRepostory.Sil(Emlak);
            _EmlakRepostory.Kaydet();
            TempData["basarili"] = "Kayıt başarıyla silindi!";
            return RedirectToAction("Index", "Emlak");

        }
    }
}
