USE [master]

IF db_id('IfIFitz') IS NULl
  CREATE DATABASE [IfIFitz]
GO

USE [IfIFitz]
GO


DROP TABLE IF EXISTS [Comment];
DROP TABLE IF EXISTS [Favorite];
DROP TABLE IF EXISTS [Post];
DROP TABLE IF EXISTS [Size];
DROP TABLE IF EXISTS [Material];
DROP TABLE IF EXISTS [UserProfile];
GO


CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] nvarchar(28) NOT NULL,
  [Name] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [ImageLocation] nvarchar(255),
  [IsActive] bit NOT NULL
)
GO

CREATE TABLE [Post] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [Title] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL,
  [ImageLocation] nvarchar(255),
  [CreatedDateTime] datetime NOT NULL,
  [SizeId] int NOT NULL,
  [MaterialId] int NOT NULL
)
GO

CREATE TABLE [Favorite] (
  [UserProfileId] int NOT NULL,
  [PostId] int NOT NULL,
  PRIMARY KEY (UserProfileId, PostId)
)
GO

CREATE TABLE [Size] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Material] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Type] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Comment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [PostId] int NOT NULL,
  [UserProfileId] int NOT NULL,
  [Content] nvarchar(255) NOT NULL,
  [CreatedDateTime] datetime NOT NULL
)
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([SizeId]) REFERENCES [Size] ([Id])
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([MaterialId]) REFERENCES [Material] ([Id])
GO

ALTER TABLE [Favorite] ADD FOREIGN KEY ([PostId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Favorite] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([PostId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
GO