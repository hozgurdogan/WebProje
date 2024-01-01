using RezervasyonUcak.Models;
using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class Bilet
    {
        [Key]

        private int id;
        private  User musteri;
        private DateTime kesimTarihi;
        private bool iptalMi;
        private  Koltuk koltuk;
        private  UcusSefer ucusSefer;
        private double biletFiyat;

        public virtual UcusSefer UcusSefer { get => ucusSefer; set => ucusSefer = value; }
        public int Id { get => id; set => id = value; }
        public virtual User Musteri { get => musteri; set => musteri = value; }
        public DateTime KesimTarihi { get => kesimTarihi; set => kesimTarihi = value; }
        public double BiletFiyat { get => biletFiyat; set => biletFiyat = value; }
        public bool IptalMi { get => iptalMi; set => iptalMi = value; }
        public virtual Koltuk Koltuk { get => koltuk; set => koltuk = value; }
    }
}
