using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
    public class UcusSefer
    {
        [Key]
        public int UcusId { get; set; }

        private string baslangicSaat;
        private string varisSaati;
        private Ucak ucak;
        private UcusKonum ucusKonum;
        private double ucusFiyat;
        public UcusKonum UcusKonum { get => ucusKonum; set => ucusKonum = value; }



     
        public string BaslangicSaat { get => baslangicSaat; set => baslangicSaat = value; }
        public string VarisSaati { get => varisSaati; set => varisSaati = value; }
        public Ucak Ucak { get => ucak; set => ucak = value; }
        public double UcusFiyat { get => ucusFiyat; set => ucusFiyat = value; }
    }
}
