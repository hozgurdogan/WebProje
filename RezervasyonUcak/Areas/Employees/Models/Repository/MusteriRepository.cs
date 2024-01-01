using RezervasyonUcak.Areas.Employees.Models;
using RezervasyonUcak.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public class MusteriRepository :ImusteriRepository
    {


        readonly AppDbContext _context;

        public MusteriRepository(AppDbContext context)
        {
            _context = context;
        }
    

     

        public bool existByUsername(string username)
        {

            return _context.Musteri.Any(musteri => musteri.User.Surname == username);

        }

        public List<User> getAllEmployee(string usernameOrMail)
        {
            throw new NotImplementedException();
        }

        public User getEmployeeByUsernameAndPassword(string mail, string password)
        {


            User user = (from _user in _context.Users
                                where _user.Email == mail &&
                                _user.Password == password
                                select _user).FirstOrDefault();


            return user;
        }

        public bool mailKontrol(string email)
        {
            return _context.Musteri.Any(musteri => musteri.User.Email == email);
        }

        public void musteriGetir(string Mail)
        {
            throw new NotImplementedException();
        }

        public User musteriGetirByMail(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public void musteriGuncelle(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        public void musteriKaydet(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        public void musteriSil(Musteri musteri)
        {
            throw new NotImplementedException();
        }

        ICollection<Musteri> ImusteriRepository.getAllEmployee(string usernameOrMail)
        {
            throw new NotImplementedException();
        }
    }
}
