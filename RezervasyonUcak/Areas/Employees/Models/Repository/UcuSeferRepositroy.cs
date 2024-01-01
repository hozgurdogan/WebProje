using Microsoft.EntityFrameworkCore;
using RezervasyonUcak.Areas.Employees.Models.Dto;

namespace RezervasyonUcak.Areas.Employees.Models.Repository
{
	public class UcuSeferRepositroy : IUcusSeferRepository
	{
		private readonly AppDbContext dbContext;
		public UcuSeferRepositroy(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

	
		public List<UcusSefer> ucusSeferleri()
		{
			List<UcusSefer> sefer = dbContext.UcusSefers.ToList();

			return dbContext.UcusSefers.ToList();
		}


        public List<UcusKonum> UcusSeferKonumGetir()
		{


				List<UcusKonum> query=	dbContext.UcusKonum.ToList();

	
			return query;
		}
		public Ucak ucakGetir(int ucakId)
		{
		Ucak ucak=	dbContext.Ucak.Include(a=>a.Firma).Where(ucak=>ucak.UcakId==ucakId).Select(ucak=>new Ucak
				{
					Firma=ucak.Firma,
					KoltukSayisi=ucak.KoltukSayisi,	
					ModelNo=ucak.ModelNo,	
					UcakId=ucak.UcakId,

			}).FirstOrDefault();
			return ucak;
		}

		


		public List<UcusSefer> UcusSeferiGetir(int konumId ,DateTime ucusTarih)

		{
            UcusTarih tarih = dbContext.Tarih.Where(tarih => tarih.UcusTarihi == ucusTarih).
				
				Select(t=>new UcusTarih { 
				
					Id=t.Id,
					UcusTarihi=t.UcusTarihi,
					Konumlar=dbContext.UcusKonum.Where(tarih=>tarih.Id==t.Id).ToList(),
				}).
				
				FirstOrDefault();

            if (tarih == null)
            {
                return null;//böyle tarihte bir uçuş yok

            }
            UcusKonum konum = dbContext.UcusKonum.Include(a=>a.Seferler).Where(konum => konum.Id == konumId && konum.Tarih == tarih).FirstOrDefault();
			

            List<UcusSefer> seferler = dbContext.UcusSefers.
				Include(ucak=>ucak.Ucak.Firma).
				Include(konum=>konum.UcusKonum).
				Include(firma => firma.Ucak.Firma).Where(s => s.UcusKonum == konum).
			Select(sefer=>new UcusSefer
			{
				UcusId=sefer.UcusId,
				Ucak=dbContext.Ucak.Where(ucak=>ucak.UcakId==sefer.Ucak.UcakId).Select(u=>new Ucak
				{
					Firma=u.Firma,
					KoltukSayisi=u.KoltukSayisi,
					ModelNo=u.ModelNo,
                    UcakId = u.UcakId,
					

				}).FirstOrDefault(),
				
				BaslangicSaat=sefer.BaslangicSaat,
				VarisSaati=sefer.VarisSaati,
				UcusKonum=sefer.UcusKonum,
				UcusFiyat=sefer.UcusFiyat,


			}).				
				ToList();

			return seferler;

        }

		public	UcusSeferResponse ucusSefer(int id)
		{
            UcusSeferResponse model = (from ucus in dbContext.UcusSefers
                                       where ucus.UcusId == id
                                       select new UcusSeferResponse
                                       {
                                           UcusId = ucus.UcusId,
                                           UcakId = ucus.Ucak.UcakId,
                                           BaslangicKonum = ucus.UcusKonum.BaslangicKonum,
                                           VarisKonum = ucus.UcusKonum.VarisKonum,
                                           BaslangicSaat = ucus.BaslangicSaat,
                                           VarisSaati = ucus.VarisSaati,
                                           UcakModelNo = ucus.Ucak.ModelNo,
                                           FirmaAdi = ucus.Ucak.Firma.FirmaAdi,
                                           UcusFiyat = ucus.UcusFiyat,
                                           koltuklar = ucus.Ucak.Koltuklar

                                       }).FirstOrDefault();

            return model;
        }


		



    }
}
