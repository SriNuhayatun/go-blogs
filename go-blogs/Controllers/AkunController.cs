using go_blogs.Helper;
using go_blogs.Models;
using go_blogs.Services.BlogServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace go_blogs.Controllers
{
    public class AkunController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailServices _email;
        public AkunController(AppDbContext context,EmailServices e)
        {
            _context = context;//_Context dimasukan konstruktor agar lebih ringkas
            _email = e;
        }
        public IActionResult Daftar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Daftar(User datanya)
        {
            int OTP = BanyakBantuan.BuatOTP();

            _email.KirimEmail(datanya.Email, "Konfirmasi Daftar", "<h1 style='color:green'>Ini Dari Saya</h1>"+OTP);

            var deklarRole = _context.Tb_Roles.Where(x=>x.Id=="1").FirstOrDefault();
            datanya.Roles = deklarRole;
            _context.Add(datanya);//sama saja dengan insert into tb_User
             _context.SaveChanges();// eksekusi

            //cara 1
            return RedirectToAction("Masuk");//menarik ke action
            //cara 2
            //return RedirectToAction(controllerName: "Akun" actionName: "Masuk"); 
        }

        public IActionResult Masuk()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Masuk(User datanya)
        {
           
            //var cari = _context.Tb_User.Where(bebas =>  //proses pencarian
            //                                  bebas.Username == datanya.Username
            //                                  &&
            //                                  bebas.Password == datanya.Password
            //).FirstOrDefault();//hanya dapat 1 data

            var cariusername = _context.Tb_User.Where(bebas =>  //proses pencarian
                                               bebas.Username == datanya.Username
             ).FirstOrDefault();
            var caripassword = _context.Tb_User.Where(bebas =>  //proses pencarian
                                               bebas.Password == datanya.Password
             ).FirstOrDefault();

            if (cariusername != null)
            {
                var cekpassword= _context.Tb_User.Where(bebas =>  //proses pencarian
                                               bebas.Username == datanya.Username
                                               &&
                                               bebas.Password == datanya.Password
                                               )
                .Include(bebas2=>bebas2.Roles).FirstOrDefault();

                if (cekpassword != null)
                {
                    //proses tampungan data 
                    var daftar = new List<Claim>
                    {
                        new Claim("Username",cariusername.Username),
                        new Claim("Role",cariusername.Roles.Name)
                    };
                    //proses daftar auth
                    await HttpContext.SignInAsync(
                        new ClaimsPrincipal(
                            new ClaimsIdentity(daftar, "Cookie", "Username", "Role")
                        )
                   );
                    if (cariusername.Roles.Id == "1")//admin
                    {
                        //return RedirectToAction(controllerName: "Blog", actionName: "Index
                        return Redirect("/Admin/Home");
                    }else if(cariusername.Roles.Id == "2")
                    {
                        return Redirect("/User/Home");
                    }
                    return RedirectToAction(controllerName: "Home", actionName: "Privacy");
                }
                ViewBag.pesan = "password salah";
                return View(datanya);
            }
            ViewBag.pesan = "Username salah";
            return View(datanya);

            //if (cari != null)
            //{
            //    return RedirectToAction(controllerName: "Blog", actionName:"Index");
            //}
            //return View(datanya);

        }
        
        //tugas
        //cari tahu kalu misal username dan password tidak tersedia
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


    }
}
