CREATE TABLE [dbo].[Event]
(
	[Id] int primary key identity,
	[Name] nvarchar(128) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [ImageUrl] NVARCHAR(1024) NULL,
)
