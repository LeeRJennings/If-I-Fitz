using IfIFitz.Models;
using IfIFitz.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public class SizeRepository : BaseRepository, ISizeRepository
    {
        public SizeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Size> GetAllSizes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                                        FROM Size";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var sizes = new List<Size>();
                        while (reader.Read())
                        {
                            sizes.Add(new Size()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            });
                        }
                        return sizes;
                    }
                }
            }
        }
    }
}
