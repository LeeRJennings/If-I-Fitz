using IfIFitz.Models;
using IfIFitz.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }

        public List<Comment> GetCommentsByPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.PostId, c.UserProfileId, c.Content, c.CreatedDateTime, 

                                               u.Name, u.ImageLocation, u.IsActive

                                        FROM Comment c
                                        JOIN UserProfile u ON u.Id = c.UserProfileId
                                        WHERE c.PostId = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var comments = new List<Comment>();
                        while (reader.Read())
                        {
                            comments.Add(new Comment()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                PostId = DbUtils.GetInt(reader, "PostId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Content = DbUtils.GetString(reader, "Content"),
                                CreatedDateTime = DbUtils.GetDateTime(reader, "CreatedDateTime"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                }
                            });
                        }
                        return comments;
                    }
                }
            }
        }
    }
}
