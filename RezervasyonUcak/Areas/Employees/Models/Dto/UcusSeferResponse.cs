namespace RezervasyonUcak.Areas.Employees.Models.Dto
{
    public class UcusSeferResponse
    {

        public int UcusId { get; set; }
        public int UcakId { get; set; } 
        public string BaslangicKonum { get; set; }
        public string VarisKonum { get; set; }
        public string BaslangicSaat { get; set; }
        public string VarisSaati { get; set; }
        public string UcakModelNo { get; set; }
        public string FirmaAdi { get; set; }
        public double UcusFiyat { get; set; }
       public List<Koltuk> koltuklar {  get; set; }
    }
}
