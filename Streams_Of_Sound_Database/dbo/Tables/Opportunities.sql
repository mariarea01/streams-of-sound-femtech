﻿CREATE TABLE [dbo].[Opportunities]
(
	[Id]                INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name]              NVARCHAR(200) NOT NULL, 
    [Description]       NVARCHAR(MAX) NOT NULL,  
    [StartTime]         DATETIMEOFFSET NOT NULL, 
    [EndTime]           DATETIMEOFFSET NOT NULL,
    [Address]           NVARCHAR(50) NOT NULL, 
    [Address1]          NVARCHAR(50) NULL, 
    [City]              NVARCHAR(50) NOT NULL, 
    [Zip]               NVARCHAR(50) NOT NULL, 
    [State]             NVARCHAR(50) NOT NULL,
    [SlotsAvailable]    INT NOT NULL, 
    [isArchived]        BIT NULL,

)
