USE [ChamThiOnline]
GO
SET IDENTITY_INSERT [dbo].[LogThi] ON 

GO
INSERT [dbo].[LogThi] ([Id], [DeId], [Diem], [SBD], [CreatedDate], [UserId], [NgayThi]) VALUES (1, 1, 0, N'1201120', NULL, 3, NULL)
GO
INSERT [dbo].[LogThi] ([Id], [DeId], [Diem], [SBD], [CreatedDate], [UserId], [NgayThi]) VALUES (2, 2, 0, N'0132750', NULL, 3, NULL)
GO
INSERT [dbo].[LogThi] ([Id], [DeId], [Diem], [SBD], [CreatedDate], [UserId], [NgayThi]) VALUES (3, 3, 0, N'0000000', NULL, 3, NULL)
GO
SET IDENTITY_INSERT [dbo].[LogThi] OFF
GO



ALTER TABLE LogThi
ALTER COLUMN Diem float null;