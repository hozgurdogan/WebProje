using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Areas.Employees.Models.Repository;
using RezervasyonUcak.Models;
using System.Security.Claims;

namespace RezervasyonUcak.Controllers.AuthController
{
    // [ApiController]
    //  [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext appDbContext;

        private readonly ImusteriRepository musteriRepository;
        private readonly UserRepository userRepository;
        private readonly IUcusSeferRepository ucusSeferRepository;

        public AuthController(AppDbContext appDbContext, ImusteriRepository musteriRepository, UserRepository userRepository, IUcusSeferRepository ucusSeferRepository)
        {
            this.appDbContext = appDbContext;
            this.musteriRepository = musteriRepository;
            this.userRepository = userRepository;
            this.ucusSeferRepository = ucusSeferRepository;
        }


        public IActionResult Login()
        {
            return View();
        }




        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Tüm yetkilendirme bilgilerini siler
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> _Login(Login login)
        {


            if (ModelState.IsValid)
            {

                User user = userRepository.getEmployeeByUsernameAndPassword(login.Mail, login.Password);


                if (user != null)
                {

                    var claims = new List<Claim>
                {

                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    if (user.Email == "furkan@gmail.com" &&user.Role==Role.Admin)
                    {

                        return RedirectToAction("Anasayfa", "AdminPanel", new { area = "Admin", });
                    }
                    else
                    {

                        return RedirectToAction("Anasayfa", "Musteri", new { area = "Employees", });


                    }

                }

            }


            ViewBag.LoginErrorMessage = "Mail ya da şifre hatalı";
            return View("Login");


        }



        [HttpPost]
        public IActionResult _Register(Register register)
        {



            if (ModelState.IsValid)
            {
                bool mailKontrol = userRepository.exisByMailAndDeletedFalse(register.Mail);
                bool usernameControl = userRepository.exisByUsernameAndDeletedFalse(register.Username);

                if (mailKontrol)
                {
                    ModelState.AddModelError("Email", "Bu mail zaten kayıtlı");
                }
                if (usernameControl)
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kayıtlı");
                }

                if (!mailKontrol)
                {

                    User user = new User();
                    user.Name = register.Name;
                    user.Surname = register.Surname;
                    user.Email = register.Mail;
                    user.Password = register.Password;
                    user.Role = Role.User;

                    appDbContext.Users.Add(user);
                    Musteri musteri = new Musteri();
                    musteri.User = user;



                    appDbContext.Musteri.Add(musteri);
                    appDbContext.SaveChanges();
                    ViewBag.Message = "Kayıt işlemi başarılı.Lütfen giriş yapınız.";
                    return View("Login");

                }

            }

            return View("Register");
        }



    }
    


    }



















