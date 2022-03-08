using go_blogs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Repositories.BlogRepository
{
    public class BlogRepository:IBlogRepository
    {
        private readonly AppDbContext _blogDB;

        public BlogRepository(AppDbContext b)
        {
            _blogDB = b;
        }

        public async Task<bool> BuatBlogAsync(Blog datanya)
        {
            _blogDB.Tb_blog.Add(datanya);
            await _blogDB.SaveChangesAsync();
            return true;
        }

        public async Task<User> CariUserByUsernameAsync(string username)
        {
            return await _blogDB.Tb_User.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> HapusBlogAsync(Blog datanya)
        {
            _blogDB.Tb_blog.Remove(datanya);
            await _blogDB.SaveChangesAsync();
            return true;
        }

        public async Task<Blog> TampilBlogByIDAsync(int id)
        {
            return await _blogDB.Tb_blog.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Blog>> TampilSemuaBlogAsync()
        {
            var data = await _blogDB.Tb_blog.ToListAsync();
            return data;
        }

        public async Task<bool> UpdateBlogAsync(Blog datanya)
        {
            _blogDB.Tb_blog.Update(datanya);
            await _blogDB.SaveChangesAsync();
            return true;
        }
    }
}
