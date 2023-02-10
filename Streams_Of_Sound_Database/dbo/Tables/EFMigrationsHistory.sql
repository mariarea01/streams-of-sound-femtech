CREATE TABLE [dbo].[EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK_EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
); 
