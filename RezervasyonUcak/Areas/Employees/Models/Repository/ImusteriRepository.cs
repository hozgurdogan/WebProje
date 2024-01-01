using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public interface ImusteriRepository
    {

        void musteriKaydet(Musteri musteri);
        void musteriGetir(string Mail);
        ICollection<Musteri> getAllEmployee(string usernameOrMail);

        void musteriSil(Musteri musteri);
        void musteriGuncelle(Musteri musteri);
        bool mailKontrol(string email);


        User musteriGetirByMail(string mail, string password);
    }
}

