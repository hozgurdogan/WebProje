using System.ComponentModel.DataAnnotations;

namespace RezervasyonUcak.Areas.Employees.Models
{
	public class UcusTarih
	{

		[Key]
		private int id;

		private DateTime ucusTarih;

		private ICollection<UcusKonum> konumlar;

		public int Id { get => id; set => id = value; }
		public DateTime UcusTarihi { get => ucusTarih; set => ucusTarih = value; }
		public ICollection<UcusKonum> Konumlar { get => konumlar; set => konumlar = value; }
	}
}
