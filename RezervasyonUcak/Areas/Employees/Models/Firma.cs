using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class Firma
    {
        [Key]
        private int firmaId;


        private string firmaAdi;
        private ICollection<Ucak> ucaklar;



        public string FirmaAdi { get => firmaAdi; set => firmaAdi = value; }
        public ICollection<Ucak> Ucaklar { get => ucaklar; set => ucaklar = value; }
        public int FirmaId { get => firmaId; set => firmaId = value; }
    }
}
