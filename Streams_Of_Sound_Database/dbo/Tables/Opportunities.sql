CREATE TABLE [dbo].[Opportunities]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(200) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [DateTime] DATETIME NULL, 
    [Duration] INT NULL, 
    [Address] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Zip] INT NULL, 
    [State] NVARCHAR(50) NULL, 
    [SlotsOpenings] INT NULL, 
    [SlotsAvailable] INT NULL,
    [UserId]  NVARCHAR (450) NULL,

    CONSTRAINT [FK_Opportunity_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) 
)
