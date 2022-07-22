using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface ISizeRepository
    {
        List<Size> GetAllSizes();
    }
}