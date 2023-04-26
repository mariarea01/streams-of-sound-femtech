CREATE TABLE [dbo].[InstrumentSignUp]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[InstrumentSlotsId] INT NULL,
	[UserId] UNIQUEIDENTIFIER NULL, 

    CONSTRAINT [FK_InstrumentSignUp_InstrumentSlots] FOREIGN KEY (InstrumentSlotsId) REFERENCES [InstrumentsSlots](Id), 
	CONSTRAINT [FK_InstrumentSignUp_AspNetUsers] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id), 

    
)
