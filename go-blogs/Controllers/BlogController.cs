using go_blogs.Helper;
using go_blogs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Controllers
{
    //construktor=fungsi yang dijalankan pertama kali
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;//_Context dimasukan konstruktor agar lebih ringkas
        }

        public IActionResult Index()
        {
            var banyakdata = new BlogDashboard();

            banyakdata.blog = _context.Tb_blog.Include(x => x.User).ToList();

            banyakdata.user = _context.Tb_User.Include(x => x.Roles).ToList();

            return View(banyakdata);
            
        }

        public IActionResult Create()//UNTUK MENAMPILKAN HALAMAN YANG AKAN DIISI(kolom inputan)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog Parameter)//mnerima halaman yg akan diisi(inputan/proses)
        {
            //cara membut id
            //1.parameter.Id("contoh");
            //2.parameter.Id = Parameter.CreateDate.Ticks.ToString("x");

            //proses masukan ke database
            if (ModelState.IsValid)//untuk mengetahui suatu inputan itu valid atau tidak
            {
                //Parameter.Id = BuatPrimary.buatPrimary();cara memanggil file helper
                string nama = User.GetUsername();
                Parameter.User = _context.Tb_User.FirstOrDefault(x => x.Username == nama);


                _context.Add(Parameter);//mengganti dari insert into
                await _context.SaveChangesAsync();// menyimpan perubahan

                //asalnya url https://localhost:5091
                //menjadi https://localhost:5091/home 

                //return Ok(Parameter);
                return RedirectToAction("Index");
            }
            return View(Parameter); 
        }

        public async Task<IActionResult> Hapus(int id)
        {
            var cari = _context.Tb_blog.Where(x => x.Id == id).FirstOrDefault();
            if (cari == null)
            {
                return NotFound();
            }
            _context.Tb_blog.Remove(cari);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
