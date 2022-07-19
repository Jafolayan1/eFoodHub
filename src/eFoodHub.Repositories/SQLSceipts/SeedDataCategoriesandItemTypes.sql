GO
SET IDENTITY_INSERT [dbo].[Categories] ON
GO
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (1, N'Pizza', N'Pizza')
GO
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (2, N'Burger', N'Burger')
GO
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (3, N'Pasta', N'Pasta')
GO
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (4, N'Fries', N'Fries')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF

------------------------------------------
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] ON
GO
INSERT [dbo].[ItemTypes] (ItemTypeId, [Name]) VALUES (1, N'Veg')
GO
INSERT [dbo].[ItemTypes] (ItemTypeId, [Name]) VALUES (2, N'NonVeg')
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] OFF
------------------------------------------
SET IDENTITY_INSERT [dbo].[AspNetRoles] ON 
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (1, N'Admin', N'ADMIN', N'979797')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (2, N'User', N'USER', N'979799')
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF