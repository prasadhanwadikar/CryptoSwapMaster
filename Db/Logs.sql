CREATE TABLE [dbo].[Logs] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME       NOT NULL,
	[Logger]    VARCHAR (255)  NOT NULL,
	[Level]     VARCHAR (50)   NOT NULL,
    [Thread]    VARCHAR (255)  NOT NULL,
    [Message]   VARCHAR (4000) NOT NULL,
    [Exception] VARCHAR (2000) NULL
);

