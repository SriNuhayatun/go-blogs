using go_blogs.Helper;
using go_blogs.Models;
using go_blogs.Repositories.BlogRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Services.BlogServices
{
    public class BlogServices : IBlogServices
    {
        private readonly IBlogRepository _blogRepo;
        private readonly FileService _file;

        public BlogServices(IBlogRepository r,FileService f)
        {
            _blogRepo = r;
            _file = f;

        }

        public async Task<bool> BuatBlog(Blog datanya, string username, IFormFile Fotonya)
        {
            //datanya.Id = BuatPrimary.buatPrimary();
            var CariUser = await _blogRepo.CariUserByUsernameAsync(username);
            datanya.User = CariUser;

            datanya.Image = await _file.SimpanFile(Fotonya);

            return await _blogRepo.BuatBlogAsync(datanya);
        }

        public async Task<bool> HapusBlog(int id)
        {
            var cari = await _blogRepo.TampilBlogByIDAsync(id);
            await _blogRepo.HapusBlogAsync(cari);
            return true;
        }

        public async Task<Blog> TampilBlogById(int id)
        {
            return await _blogRepo.TampilBlogByIDAsync(id);
        }

        public async Task<List<Blog>> TampilSemuaData()
        {
            return await _blogRepo.TampilSemuaBlogAsync();
        }

        public async Task<bool> UpdateBlogAsync(Blog datanya)
        {
            return await _blogRepo.UpdateBlogAsync(datanya);

        }
    }
}
