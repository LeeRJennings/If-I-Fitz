﻿USE [IfIFitz]
GO

set identity_insert [Size] on;
INSERT INTO "Size" ("Id", "Name")
VALUES
(1, 'Small'),
(2, 'Medium'),
(3, 'Large')
set identity_insert [Size] off;

set identity_insert [Material] on;
INSERT INTO "Material" ("Id", "Type")
VALUES
(1, 'Cardboard'),
(2, 'Plastic'),
(3, 'Cloth'),
(4, 'Metal'),
(5, 'Wicker'),
(6, 'Other')
set identity_insert [Material] off;

set identity_insert [UserProfile] on;
INSERT INTO "UserProfile" ("Id", "FirebaseUserId", "Name", "Email", "ImageLocation", "IsActive")
VALUES
(1, 'AqdwI6RxiudFGKncHBpxKdPoLwg1', 'Davos', 'davos@nss.com', NULL, 1),
(2, 'hrXadtyoYFcWPw6YVs5ZvFoqXwb2', 'Paul', 'paul@nss.com', NULL, 1),
(3, 'nMntE5icTbM6SRbuPZIIbl9MdGx2', 'Teacup', 'teacup@nss.com', NULL, 1)
set identity_insert [UserProfile] off;

set identity_insert [Post] on;
INSERT INTO "Post" ("Id", "UserProfileId", "Title", "Description", "ImageLocation", "CreatedDateTime", "SizeId", "MaterialId")
VALUES
(1, 1, 'My Favorite Box Ever', 'Of all the boxes I''ve ever sat in this one is the best!! The vibes in this box are immaculate.', 'https://res.cloudinary.com/leerjennings/image/upload/v1659128908/IfIFitz/Davos/davosResize2_q4q0gr.jpg', '2022-07-16', 2, 1),
(2, 3, 'Amazon makes a great box', 'So comfy, loves this box. Paul better not try to swipe this one from me!', 'https://res.cloudinary.com/leerjennings/image/upload/v1659128976/IfIFitz/Teacup/teacupResize1_fq17mj.jpg', '2022-07-18', 2, 1),
(3, 2, 'Not all good boxes are cardboard', 'This box/basket is DOPE!! Gotta give that wicker a shot if you know what''s up.', 'https://res.cloudinary.com/leerjennings/image/upload/v1659128949/IfIFitz/Paul/paulResize2_dawvai.jpg', '2022-07-20', 3, 5)
set identity_insert [Post] off;

INSERT INTO "Favorite" ("UserProfileId", "PostId")
VALUES
(1, 3),
(2, 2),
(3, 1)

set identity_insert [Comment] on;
INSERT INTO "Comment" ("Id", "PostId", "UserProfileId", "Content", "CreatedDateTime")
VALUES
(1, 1, 2, 'Is it really that great? Looks like a normal box to me.', '2022-07-19'),
(2, 1, 1, 'YEAH it is. Like I said the vibes are IMMACULATE.', '2022-07-20'),
(3, 3, 3, 'I''d love to try out that wicker basket! ..... if you''d quit hogging it', '2022-07-21')
set identity_insert [Comment] off;