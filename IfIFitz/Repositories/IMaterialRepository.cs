using IfIFitz.Models;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public interface IMaterialRepository
    {
        List<Material> GetAllMaterials();
    }
}