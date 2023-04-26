CREATE TABLE [dbo].[ReasonToCancel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ReasonToCancel] NVARCHAR(MAX) NOT NULL,
	InstrumentSignUpId INT NOT NULL,

	CONSTRAINT [FK_ReasonToCancel_InstrumentSignUp] FOREIGN KEY (InstrumentSignUpId) REFERENCES [InstrumentSignUp](Id),
)
