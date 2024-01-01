using RezervasyonUcak.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace RezervasyonUcak.Areas.Admin.Model.Dto
{
    public class UserRequest
    {

        [Required(ErrorMessage ="Lütfen isim giriniz")]
       
        private string name;

        [Required(ErrorMessage = "Lütfen Mail giriniz")]
        [EmailAddress(ErrorMessage ="Geçerli bir mail giriniz")]
        private string email;
        [Required(ErrorMessage = "Lütfen Şifre giriniz")]

        private string password;
        [Required(ErrorMessage = "Lütfen Soyisim giriniz")]

        private string surname;
        [Required(ErrorMessage = "Lütfen Kullanıcı rolünü seçiniz")]

        private Role role;

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Surname { get => surname; set => surname = value; }
        public Role Role { get => role; set => role = value; }
    }
}
