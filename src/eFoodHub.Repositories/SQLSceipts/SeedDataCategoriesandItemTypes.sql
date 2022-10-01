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
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description]) VALUES (5, N'Salad', N'Salad')
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
