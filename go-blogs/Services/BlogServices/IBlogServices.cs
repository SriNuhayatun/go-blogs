using go_blogs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Services.BlogServices
{
    public interface IBlogServices
    {
        Task<List<Blog>> TampilSemuaData();
        Task<Blog> TampilBlogById(int id);
        Task<bool> BuatBlog(Blog datanya, string username,IFormFile Fotonya);
        Task<bool> HapusBlog(int id);
        Task<bool> UpdateBlogAsync(Blog datanya);
    }
}
