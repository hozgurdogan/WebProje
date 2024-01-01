using Microsoft.AspNetCore.Identity;
using RezervasyonUcak.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezervasyonUcak.Areas.Employees.Models
{
    [Table("Employee")]
    public class Musteri 
    {
        [Key]
        private int id;


        private User user;
        private ICollection<Bilet> biletler;
        public ICollection<Bilet> Biletler { get => biletler; set => biletler = value; }
		public User User { get => user; set => user = value; }
        public int Id { get => id; set => id = value; }
    }
}