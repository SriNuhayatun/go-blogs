using go_blogs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Controllers
{
    public class MobilController : Controller
    {
        public IActionResult Index()
        {
            //cara deklarasi 1
            var mobils = new List<Mobil>();

            //cara deklarasi 2
            //lebih cepat dibanding deklarasi variabel lainnya
            //IEnumerable<Mobil> mobils2 = new List<Mobil>();

            //cara deklarasi 3(untuk tipe data list)
            //List<Mobil> mobils3 = new List<Mobil>();

            //mobils.Add(new Mobil
            //{
            //    IDRegistrasi = 1,
            //    Tipe = "Sedan",
            //    Merk = "Toyota",
            //    Varian = "Apple",
            //});
            //mobils.Add(new Mobil
            //{
            //    IDRegistrasi = 2,
            //    Tipe = "Bus",
            //    Merk = "Toyota",
            //    Varian = "Android",
            //});

            //string nama = "Atun";

            ////cara 1 menampung data
            //ViewBag.namaSaya = nama;
            //ViewBag.mobils = mobils;

            //////cara 2 menampung data
            //ViewData["nama"] = "Atuuun";

            var banyakMobil = new Mobil[]
            {
               new Mobil{ IDRegistrasi=1,Tipe="Sedan",Merk="Toyota",Varian="FT86"},
               new Mobil{ IDRegistrasi=2,Tipe="SUV",Merk="Toyota",Varian="RAV4"},
               new Mobil{ IDRegistrasi=3,Tipe="Sedan",Merk="Honda",Varian="Accord"},
               new Mobil{ IDRegistrasi=4,Tipe="SUV",Merk="Honda",Varian="CRV"},
               new Mobil{ IDRegistrasi=5,Tipe="Sedan",Merk="Honda",Varian="City"},

            };
            //var cariMobil = banyakMobil.Where(x => x.Tipe=="Sedan");
            //var pertama = banyakMobil.Where(x => x.Merk == "Honda").FirstOrDefault();
            //ViewBag.mobils = pertama;

            //var kedua = banyakMobil.Where(x => x.Tipe == "Sedan" && x.Merk == "Honda");
            //ViewBag.mobils = kedua;

            //var ketiga = banyakMobil.Where(x => x.Merk == "Honda" && x.Varian == "City").FirstOrDefault();
            //ViewBag.mobils = ketiga;

            var keempat = banyakMobil.Where(x => x.Merk == "Toyota");
            ViewBag.mobils = keempat;

            //var kelima = banyakMobil;
            return View();
        }
    }
}
