using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Admin.Model.Dto
{
    public class UcakEkleRequest
    {

        [Required(ErrorMessage ="Model numarası giriniz.")]
        [Display(Name = "Model Numarası")]
        private string modelNo;
        [Required(ErrorMessage = "Uçak firması giriniz.")]
        [Display(Name = "Firma")]

        private int  firmaId;
        [Required(ErrorMessage = "Koltuk sayısı giriniz.")]
        [Display(Name = "Koltuk Sayısı")]


        private int koltukSayisi;

        public int KoltukSayisi { get => koltukSayisi; set => koltukSayisi = value; }
        public int  FirmaId { get => firmaId; set => firmaId = value; }
        public string ModelNo { get => modelNo; set => modelNo = value; }
    }
}
