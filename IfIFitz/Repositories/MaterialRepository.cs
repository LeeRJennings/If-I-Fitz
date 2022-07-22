using IfIFitz.Models;
using IfIFitz.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public class MaterialRepository : BaseRepository, IMaterialRepository
    {
        public MaterialRepository(IConfiguration configuration) : base(configuration) { }

        public List<Material> GetAllMaterials()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                                        FROM Material";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var materials = new List<Material>();
                        while (reader.Read())
                        {
                            materials.Add(new Material()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Type = DbUtils.GetString(reader, "Type")
                            });
                        }
                        return materials;
                    }
                }
            }
        }
    }
}
