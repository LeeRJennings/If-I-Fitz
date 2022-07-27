using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByPostId(int id);
        Comment GetCommentById(int id);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
    }
}