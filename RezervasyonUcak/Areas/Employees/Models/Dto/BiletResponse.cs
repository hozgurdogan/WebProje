namespace RezervasyonUcak.Areas.Employees.Models.Dto
{
    public class BiletResponse
    {



  

            public int Id { get; set; }
            public string YolcuAdi { get; set; }
            public string YolcuSoyadi { get; set; }
            public string YolcuMail { get; set; }
            public DateTime BiletKesimTarihi { get; set; }
            public string BaslangicSaat { get; set; }
            public string BitisSaat { get; set; }
            public string FirmaAdi { get; set; }
            public string BaslangicKonum { get; set; }
            public string VarisKonum { get; set; }
            public string KoltukNo { get; set; }
            public string UcakModelNo { get; set; }
            public double BiletFiyat { get; set; }
            public DateTime UcusTarihi { get; set; }
        public bool Iptal {  get; set; }    
   




}
}
