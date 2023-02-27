CREATE TABLE [dbo].[Opportunities]
(
	[Id]                INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name]              NVARCHAR(200) NULL, 
    [Description]       NVARCHAR(MAX) NULL, 
    [DateTime]          DATETIME NULL, 
    [StartTime]         DATETIME NULL, 
    [EndTime]           DATETIME NULL,
    [Address]           NVARCHAR(50) NULL, 
    [City]              NVARCHAR(50) NULL, 
    [Zip]               INT NULL, 
    [State]             NVARCHAR(50) NULL, 
    [SlotsOpenings]     INT NULL, 
    [SlotsAvailable]    INT NULL,
    [UserId]            UNIQUEIDENTIFIER NULL,

    CONSTRAINT [FK_Opportunity_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) 
)
