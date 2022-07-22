using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAllPosts();
        Post GetPostById(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
        List<Post> GetPostsByUserId(int id);
        List<Post> GetUsersFavoritedPosts(int id);
        void AddFavorite(int userProfileId, int postId);
    }
}