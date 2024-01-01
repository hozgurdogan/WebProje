using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezervasyonUcak.Areas.Employees.Models
{
    
    public class Koltuk
    {
        [Key]
        private int id;
        
        private string koltukNo;
        private bool doluMu;

        public string KoltukNo { get => koltukNo; set => koltukNo = value; }
        public bool DoluMu { get => doluMu; set => doluMu = value; }
        public int Id { get => id; set => id = value; }
    }
}
