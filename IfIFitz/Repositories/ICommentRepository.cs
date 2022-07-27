using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByPostId(int id);
    }
}