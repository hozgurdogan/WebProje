using RezervasyonUcak.Areas.Employees.Models;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
    public interface IUcusSeferRepository
    {

        List<UcusKonum> UcusSeferKonumGetir();

         Ucak ucakGetir(int ucakId);
         List<UcusSefer> UcusSeferiGetir(int konumId, DateTime ucusTarih);

        List<UcusSefer> ucusSeferleri();




    }
}
