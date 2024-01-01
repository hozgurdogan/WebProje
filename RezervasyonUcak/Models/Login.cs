using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Models
{
    public class Login
    {
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail giriniz")]
        [Required(ErrorMessage = "E-Mail Boş Bırakılamaz")]

        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

    }
}
