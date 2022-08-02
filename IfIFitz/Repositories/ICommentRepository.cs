using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface ICommentRepository
    {
        Comment GetCommentById(int id);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
    }
}