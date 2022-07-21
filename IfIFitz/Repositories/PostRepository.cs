﻿using IfIFitz.Models;
using IfIFitz.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IfIFitz.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }

        public List<Post> GetAllPosts()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.Id, p.UserProfileId, p.Title, p.Description, p.ImageLocation AS PostImage, p.CreatedDateTime, p.SizeId, p.MaterialId,

	                                           u.Name AS UserName, u.ImageLocation AS UserImage, u.IsActive,

	                                           s.Name AS Size, m.Type AS Material 

                                        FROM Post p
                                        JOIN UserProfile u ON u.Id = p.UserProfileId
                                        JOIN Size s ON s.Id = p.SizeId
                                        JOIN Material m ON m.Id = p.MaterialId
                                        ORDER BY p.CreatedDateTime DESC";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var posts = new List<Post>();
                        while (reader.Read())
                        {
                            posts.Add(new Post()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageLocation = DbUtils.GetString(reader, "PostImage"),
                                CreatedDateTime = DbUtils.GetDateTime(reader, "CreatedDateTime"),
                                SizeId = DbUtils.GetInt(reader, "SizeId"),
                                MaterialId = DbUtils.GetInt(reader, "MaterialId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "UserName"),
                                    ImageLocation = DbUtils.GetString(reader, "UserImage"),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                },
                                Size = new Size()
                                {
                                    Id = DbUtils.GetInt(reader, "SizeId"),
                                    Name = DbUtils.GetString(reader, "Size")
                                },
                                Material = new Material()
                                {
                                    Id = DbUtils.GetInt(reader, "MaterialId"),
                                    Type = DbUtils.GetString(reader, "Material")
                                }
                            });
                        }
                        return posts;
                    }
                }
            }
        }

        public Post GetPostById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.Id, p.UserProfileId, p.Title, p.Description, p.ImageLocation AS PostImage, p.CreatedDateTime, p.SizeId, p.MaterialId,

	                                           u.Name AS UserName, u.ImageLocation AS UserImage, u.IsActive,

	                                           s.Name AS Size, m.Type AS Material 

                                        FROM Post p
                                        JOIN UserProfile u ON u.Id = p.UserProfileId
                                        JOIN Size s ON s.Id = p.SizeId
                                        JOIN Material m ON m.Id = p.MaterialId
                                        WHERE p.Id = @id";
                    DbUtils.AddParameter(cmd, "id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Post post = null;
                        if (reader.Read())
                        {
                            post = new Post()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageLocation = DbUtils.GetString(reader, "PostImage"),
                                CreatedDateTime = DbUtils.GetDateTime(reader, "CreatedDateTime"),
                                SizeId = DbUtils.GetInt(reader, "SizeId"),
                                MaterialId = DbUtils.GetInt(reader, "MaterialId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "UserName"),
                                    ImageLocation = DbUtils.GetString(reader, "UserImage"),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                },
                                Size = new Size()
                                {
                                    Id = DbUtils.GetInt(reader, "SizeId"),
                                    Name = DbUtils.GetString(reader, "Size")
                                },
                                Material = new Material()
                                {
                                    Id = DbUtils.GetInt(reader, "MaterialId"),
                                    Type = DbUtils.GetString(reader, "Material")
                                }
                            };
                        }
                        return post;
                    }
                }
            }
        }

        public void AddPost(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Post (UserProfileId, Title, Description, ImageLocation, CreatedDateTime, SizeId, MaterialId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@UserProfileId, @Title, @Description, @ImageLocation, @CreatedDateTime, @SizeId, @MaterialId)";
                    DbUtils.AddParameter(cmd, "@UserProfileId", post.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Title", post.Title);
                    DbUtils.AddParameter(cmd, "@Description", post.Description);
                    DbUtils.AddParameter(cmd, "@ImageLocation", post.ImageLocation);
                    DbUtils.AddParameter(cmd, "@CreatedDateTime", post.CreatedDateTime);
                    DbUtils.AddParameter(cmd, "@SizeId", post.SizeId);
                    DbUtils.AddParameter(cmd, "@MaterialId", post.MaterialId);

                    int Id = (int)cmd.ExecuteScalar();
                    post.Id = Id;
                }
            }
        }

        public void UpdatePost(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Post
                                        SET Title = @Title,
                                            Description = @Description,
                                            ImageLocation = @ImageLocation,
                                            SizeId = @SizeId,
                                            MaterialId = @MaterialId
                                        WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Title", post.Title);
                    DbUtils.AddParameter(cmd, "@Description", post.Description);
                    DbUtils.AddParameter(cmd, "@ImageLocation", post.ImageLocation);
                    DbUtils.AddParameter(cmd, "@SizeId", post.SizeId);
                    DbUtils.AddParameter(cmd, "@MaterialId", post.MaterialId);
                    DbUtils.AddParameter(cmd, "@Id", post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
