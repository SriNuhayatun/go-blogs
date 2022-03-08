using go_blogs.Helper;
using go_blogs.Models;
using go_blogs.Services.BlogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IBlogServices _serv;
        public BlogController(AppDbContext context,IBlogServices serv)
        {
            _context = context;//_Context dimasukan konstruktor agar lebih ringkas
            _serv = serv;
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
        public async Task<IActionResult> Create(Blog Parameter,IFormFile Image)//mnerima halaman yg akan diisi(inputan/proses)
        {
            //cara membut id
            //1.parameter.Id("contoh");
            //2.parameter.Id = Parameter.CreateDate.Ticks.ToString("x");

            //proses masukan ke database
            if (ModelState.IsValid)//untuk mengetahui suatu inputan itu valid atau tidak
            {
                //Parameter.Id = BuatPrimary.buatPrimary();cara memanggil file helper
                //string nama = User.GetUsername();
                //Parameter.User = _context.Tb_User.FirstOrDefault(x => x.Username == nama);//bagian service


                //_context.Add(Parameter);//mengganti dari insert into//bagian repository
                //await _context.SaveChangesAsync();// menyimpan perubahan

                //asalnya url https://localhost:5091
                //menjadi https://localhost:5091/home 
                await _serv.BuatBlog(Parameter, User.GetUsername(), Image);

                //return Ok(Parameter);
                return RedirectToAction("Index");
            }
            return View(Parameter); 
        }
        public IActionResult Details(int id)
        {

            var detail= new List<Blog>();
            var det= _context.Tb_blog.Where(x => x.Id == id).ToList();
            if (det == null)
            {
                return NotFound();
            }
            ViewBag.detail = det;
            return View();
        }
        public async Task<IActionResult> Ubah(int id)
        {
            //var cari = await _context.Tb_blog.FirstOrDefaultAsync(x => x.Id == id);
            var cari = await _serv.TampilBlogById(id);

            if (cari == null)
            {
                return NotFound();
            }

            return View(cari);
        }
        [HttpPost]
        public async Task<IActionResult> Ubah(Blog data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Tb_blog.Update(data); // query update pada sql
                    //await _context.SaveChangesAsync(); // eksekusi update
                    await _serv.UpdateBlogAsync(data);
                }
                catch
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }

            return View(data);
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
