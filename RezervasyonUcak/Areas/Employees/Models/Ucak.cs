using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;


namespace RezervasyonUcak.Areas.Employees.Models
{
    public class Ucak
    {



        [Key]
        private int ucakId;


        private  Firma firma;




        private  List<Koltuk> koltuklar;

        private string modelNo;

        private int koltukSayisi;



        public virtual Firma Firma { get => firma; set => firma = value; }
        public int UcakId { get => ucakId; set => ucakId = value; }
        public string ModelNo { get => modelNo; set => modelNo = value; }
        public int KoltukSayisi { get => koltukSayisi; set => koltukSayisi = value; }
        public  List<Koltuk> Koltuklar { get => koltuklar; set => koltuklar = value; }
    }
}
