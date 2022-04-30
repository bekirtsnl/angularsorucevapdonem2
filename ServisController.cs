using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Models;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    public class ServisController : ApiController
    {
        SORUCEVAPEntities db = new SORUCEVAPEntities();
        SonucModel sonuc = new SonucModel();

        #region soru

        [HttpGet]
        [Route("api/soruliste")]
        public List<soruModel> SoruListe()
        {
            List<soruModel> liste = db.soru.Select(x => new soruModel()
            {
                soru_Id = x.soru_Id,
                soru_adi = x.soru_adi,
                soru_katid = x.soru_katid,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/sorubyid/{soru_Id}")]
        public soruModel soruById(string soru_Id)
        {
            soruModel kayit = db.soru.Where(s => s.soru_Id == soru_Id).Select(x => new
                soruModel()
            {
                soru_Id = x.soru_Id,
                soru_adi = x.soru_adi,
                soru_katid = x.soru_katid,
            }
            ).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/soruekle")]
        public SonucModel soruEkle(soruModel model)
        {
            if (db.soru.Count(s => s.soru_adi == model.soru_adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Kayıtlıdır!";
                return sonuc;
            }
            soru yeni = new soru();
            yeni.soru_Id = Guid.NewGuid().ToString();
            yeni.soru_adi = model.soru_adi;
            yeni.soru_katid = model.soru_katid;
            db.soru.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Eklendi";


            return sonuc;
        }
        [HttpPut]
        [Route("api/soruduzenle")]
        public SonucModel soruDuzenle(soruModel model)
        {
            soru kayit = db.soru.Where(s => s.soru_Id == model.soru_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.soru_adi = model.soru_adi;
            kayit.soru_katid = model.soru_katid;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorusil/{soru_Id}")]
        public SonucModel soruSil(string soru_Id)
        {
            soru kayit = db.soru.Where(s => s.soru_Id == soru_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.soru.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Silindi";

            return sonuc;
        }

        #endregion


        #region cevap

        [HttpGet]
        [Route("api/cevapliste")]
        public List<CevapModel> CevapListe()
        {
            List<CevapModel> liste = db.Cevap.Select(x => new CevapModel()
            {
                cevap_Id = x.cevap_Id,
                cevap_adi = x.cevap_adi,
                cevap_soruid = x.cevap_soruid,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/cevapbyid/{cevap_Id}")]
        public CevapModel OgrenciById(string cevap_Id)
        {
            CevapModel kayit = db.Cevap.Where(s => s.cevap_Id == cevap_Id).Select(x => new CevapModel()
            {
                cevap_Id = x.cevap_Id,
                cevap_adi = x.cevap_adi,
                cevap_soruid = x.cevap_soruid,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/cevapekle")]
        public SonucModel CevapEkle(CevapModel model)
        {

            Cevap yeni = new Cevap();
            yeni.cevap_Id = Guid.NewGuid().ToString();
            yeni.cevap_adi = model.cevap_adi;
            yeni.cevap_soruid = model.cevap_soruid;
            db.Cevap.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/cevapduzenle")]
        public SonucModel CevapDuzenle(CevapModel model)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevap_Id == model.cevap_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            kayit.cevap_Id = model.cevap_Id;
            kayit.cevap_adi = model.cevap_adi;
            kayit.cevap_soruid = model.cevap_soruid;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/cevapsil/{cevap_Id}")]
        public SonucModel CevapSil(string cevap_Id)
        {
            Cevap kayit = db.Cevap.Where(s => s.cevap_Id == cevap_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            db.Cevap.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "cevap Silndi";
            return sonuc;
        }

        #endregion


        #region kategori

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<katModel> KategoriListe()
        {
            List<katModel> liste = db.kategori.Select(x => new katModel()
            {
                kat_Id = x.kat_Id,
                kat_adi = x.kat_adi,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{kat_Id}")]
        public katModel kategoriById(string kat_Id)
        {
            katModel kayit = db.kategori.Where(s => s.kat_Id == kat_Id).Select(x => new
                katModel()
            {
                kat_Id = x.kat_Id,
                kat_adi = x.kat_adi,
            }
            ).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel kategoriEkle(katModel model)
        {
            if (db.kategori.Count(s => s.kat_adi == model.kat_adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır!";
                return sonuc;
            }
            kategori yeni = new kategori();
            yeni.kat_Id = Guid.NewGuid().ToString();
            yeni.kat_adi = model.kat_adi;
            db.kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";


            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel kategoriDuzenle(katModel model)
        {
            kategori kayit = db.kategori.Where(s => s.kat_Id == model.kat_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.kat_adi = model.kat_adi;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kat_Id}")]
        public SonucModel kategoriSil(string kat_Id)
        {
            kategori kayit = db.kategori.Where(s => s.kat_Id == kat_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";

            return sonuc;
        }

        #endregion

    }
}
