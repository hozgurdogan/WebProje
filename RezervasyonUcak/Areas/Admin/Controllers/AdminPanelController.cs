using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RezervasyonUcak.Areas.Admin.Model.Dto;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;
using System.Security.Claims;
namespace RezervasyonUcak.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminPanelController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = HttpContext.User;
            var email = user.FindFirst(ClaimTypes.Name)?.Value;
            var currentUser = appContext.Users.Where(u => u.Email == email).FirstOrDefault();
            ViewBag.CurrentUser = currentUser;

            base.OnActionExecuting(filterContext);
        }


        private readonly AppDbContext appContext;
        private readonly ImusteriRepository musteriRepository;
        public AdminPanelController(AppDbContext appContext, ImusteriRepository imusteriRepository)
        {
            this.appContext = appContext;
            this.musteriRepository = imusteriRepository;


        }

        [HttpPost]

        public void delete(int id)
        {

            User user = appContext.Users.Where(user => user.Id == id).FirstOrDefault();

            user.Deleted = true;

            appContext.SaveChanges();


        }

        public IActionResult YetkiliKullaniciEkrani()
        {
            List<User> admins = appContext.Users.Where(user => user.Role == Role.Admin && user.Deleted == false).ToList();
            return View(admins);
        }




        public void kullaniciEkle(UserRequest userRequest)
        {

            if (ModelState.IsValid)
            {
                User user = new User();
                user.Name = userRequest.Name;
                user.Email = userRequest.Email;
                user.Password = userRequest.Password;
                user.Surname = userRequest.Surname;
                user.Role = userRequest.Role;
                user.Deleted = false;

                appContext.Users.Add(user);

                if (user.Role == Role.User)
                {
                    Musteri musteri = new Musteri();
                    musteri.User = user;
                    appContext.Musteri.Add(musteri);
                }

                appContext.SaveChanges();
            }
        }

        public List<Ucak> ucakBilgileri()
        {
            List<Ucak> ucak = appContext.Ucak.Select(

                ucak => new Ucak
                {
                    UcakId = ucak.UcakId,
                    Firma = ucak.Firma,
                    KoltukSayisi = ucak.KoltukSayisi,
                    ModelNo = ucak.ModelNo,
                }
                ).OrderBy(ucak => ucak.Firma.FirmaId).ToList();

            return ucak;
        }

        public List<Bilet> BiletGoruntule()
        {
            List<Bilet> bilet = (from a in appContext.Bilets
                                 select new Bilet
                                 {
                                     Id = a.Id,
                                     Musteri = a.Musteri,
                                     BiletFiyat = a.BiletFiyat,
                                     IptalMi = a.IptalMi,
                                     KesimTarihi = a.KesimTarihi,
                                     Koltuk = a.Koltuk,
                                     UcusSefer = a.UcusSefer,
                                     //UcusSefer = appContext.UcusSefers.Where(konum=>konum.UcusId==),

                                 }).ToList();

            return bilet;

        }


        public List<UcusSefer> ucuslariGetir()
        {
            List<UcusSefer> ucuslar = appContext.UcusSefers.Select(uc => new UcusSefer
            {

                UcusId = uc.UcusId,
                Ucak = appContext.Ucak.Where(ucak => ucak.UcakId == uc.Ucak.UcakId).Select(u => new Ucak
                {

                    UcakId = u.UcakId,
                    Firma = u.Firma,
                    ModelNo = u.ModelNo,
                    KoltukSayisi = u.KoltukSayisi,

                }).FirstOrDefault(),
                UcusKonum = appContext.UcusKonum.Where(ucak => ucak.Id == uc.UcusKonum.Id).Select(u => new UcusKonum
                {

                    Id = u.Id,
                    BaslangicKonum = u.BaslangicKonum,
                    VarisKonum = u.VarisKonum,
                    Tarih = u.Tarih,

                }).FirstOrDefault(),
                BaslangicSaat = uc.BaslangicSaat,
                UcusFiyat = uc.UcusFiyat,
                VarisSaati = uc.VarisSaati,

            }).ToList();
            return ucuslar;


        }


        public IActionResult UcusBilgileriGoruntule()
        {
            return View(ucuslariGetir());

        }
        [HttpGet]
        public List<Firma> GetUcakFirma()
        {

            return appContext.Firma.ToList();

        }
        [HttpPost]
        public IActionResult UcakEkle(UcakEkleRequest request)
        {

            if (ModelState.IsValid)
            {

                Ucak ucak = new Ucak();
                Firma firma = appContext.Firma.Where(firma => firma.FirmaId == request.FirmaId).FirstOrDefault();


                ucak.ModelNo = request.ModelNo;
                ucak.KoltukSayisi = request.KoltukSayisi;
                ucak.Firma = firma!;
                appContext.Ucak.Add(ucak);
                appContext.SaveChanges();
                ViewBag.msg = "Kayıt başarılı";
                return View("UcakEkleEkrani");

            }
            ViewBag.msg = "Kayıt başarısız";

            return View("UcakEkleEkrani");


        }

        public IActionResult UcakEkleEkrani()
        {
            return View();
        }

        public IActionResult UcakBilgileriGoruntule()
        {

            return View(ucakBilgileri());



        }

        [HttpGet]
        public List<Ucak> GetUcaklar(int firmaId)
        {
            var query = appContext.Ucak
                .Where(ucak => ucak.Firma.FirmaId == firmaId)
                .Select(ucak => new Ucak
                {
                    UcakId = ucak.UcakId,
                    Firma = ucak.Firma,
                    ModelNo = ucak.ModelNo,


                })
                .ToList();

            return query;
        }

        //Get all employee
        [Route("employee/getAll")]
        [HttpGet]
        public List<User> getAllEmployee()
        {



            List<User> musteriler = appContext.Users.Where(user => user.Role == Role.User && user.Deleted == false).ToList();



            if (musteriler == null)
            {
                return null;
            }

            //  return Ok(employees);
            return musteriler;

        }



        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult MusteriGoruntule()
        {
            List<User> employees = getAllEmployee();

            //HttpClient client = new HttpClient();

            //             client.BaseAddress=new Uri("https://localhost:7004/");
            //   client.DefaultRequestHeaders.Accept.Clear();

            // client.DefaultRequestHeaders
            //.Accept
            //.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = client.GetAsync("employee/getAll");

            //      response.Wait();

            //    var result=response.Result;

            //  if(result.IsSuccessStatusCode)
            //    {

            return View(employees);

        }




        public IActionResult MusteriEkle()
        {


            return View();
        }


        public IActionResult _MusteriEkle(UserRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                bool mailKontrol = musteriRepository.mailKontrol(userRequest.Email);

                if (mailKontrol)
                {
                    ModelState.AddModelError("Email", "Bu mail zaten kayıtlı");
                }





            }
            ViewBag.Message = "Lütfen bilgileri eksiksiz doldurun";

            return View();



        }



        public IActionResult UcakEkle()
        {


            return View();
        }


        public IActionResult UcusSeferiEkle()
        {


            return View(appContext.Firma.ToList());
        }
        public IActionResult UcusBilgiGoruntule()
        {


            return View();
        }
        public IActionResult BiletEkle()
        {


            return View();
        }
        public IActionResult BiletBilgiGoruntule()
        {


            return View();
        }



    }


}

