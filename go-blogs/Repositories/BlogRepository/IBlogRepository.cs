using go_blogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Repositories.BlogRepository
{
    public interface IBlogRepository
    {
        //CRUD
        Task<bool> BuatBlogAsync(Blog datanya);
        Task<List<Blog>> TampilSemuaBlogAsync();
        Task<Blog> TampilBlogByIDAsync(int id);
        Task<bool> UpdateBlogAsync(Blog datanya);
        Task<bool> HapusBlogAsync(Blog datanya);
        Task<User> CariUserByUsernameAsync(string username);
    }
}
