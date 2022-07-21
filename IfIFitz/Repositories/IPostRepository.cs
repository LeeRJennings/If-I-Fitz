using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        Post GetPostById(int id);
        void AddPost(Post post);
    }
}