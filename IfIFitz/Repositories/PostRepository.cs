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

	                                            s.Name AS Size, m.Type AS Material, 

		                                        c.Id AS CommentId, c.Content, c.CreatedDateTime AS CommentDate, c.UserProfileId AS CommentUserId, 
		
		                                        cu.Name CommentUserName

                                        FROM Post p
                                        JOIN UserProfile u ON u.Id = p.UserProfileId
                                        JOIN Size s ON s.Id = p.SizeId
                                        JOIN Material m ON m.Id = p.MaterialId
                                        LEFT JOIN Comment c ON c.PostId = p.Id
                                        LEFT JOIN UserProfile cu ON cu.Id = c.UserProfileId
                                        WHERE p.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Post post = null;
                        while (reader.Read())
                        {
                            if (post == null)
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
                                    },
                                    Comments = new List<Comment>()
                                };
                            }
                            if (DbUtils.IsNotDbNull(reader, "CommentId"))
                            {
                                post.Comments.Add(new Comment()
                                {
                                    Id = DbUtils.GetInt(reader, "CommentId"),
                                    PostId = DbUtils.GetInt(reader, "Id"),
                                    UserProfileId = DbUtils.GetInt(reader, "CommentUserId"),
                                    Content = DbUtils.GetString(reader, "Content"),
                                    CreatedDateTime = DbUtils.GetDateTime(reader, "CommentDate"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "CommentUserId"),
                                        Name = DbUtils.GetString(reader, "CommentUserName")
                                    }
                                });
                            }
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

                    post.Id = (int)cmd.ExecuteScalar();
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

        public void DeletePost(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Post
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Post> GetPostsByUserId(int id)
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
                                        WHERE p.UserProfileId = @id
                                        ORDER BY p.CreatedDateTime DESC";
                    DbUtils.AddParameter(cmd, "@id", id);

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

        public List<Post> GetUsersFavoritedPosts(int id)
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
                                        JOIN Favorite f ON f.PostId = p.Id
                                        WHERE f.UserProfileId = @id
                                        ORDER BY p.CreatedDateTime DESC";
                    DbUtils.AddParameter(cmd, "@id", id);

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

        public void AddFavorite(int userProfileId, int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Favorite (UserProfileId, PostId)
                                        VALUES (@userProfileId, @postId)";
                    DbUtils.AddParameter(cmd, "@userProfileId", userProfileId);
                    DbUtils.AddParameter(cmd, "@postId", postId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFavorite(int userProfileId, int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Favorite
                                        WHERE UserProfileId = @userProfileId AND PostId = @postId";
                    DbUtils.AddParameter(cmd, "@userProfileId", userProfileId);
                    DbUtils.AddParameter(cmd, "@postId", postId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Post> Search(string criterion)
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
                                        WHERE p.Title LIKE @Criterion OR 
                                              u.Name LIKE @Criterion OR 
                                              p.Description LIKE @Criterion OR 
                                              s.Name LIKE @Criterion OR 
                                              m.type LIKE @Criterion
                                        ORDER BY p.CreatedDateTime DESC";
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");

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
    }
}
