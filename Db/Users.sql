CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Ip]        NVARCHAR (25)  NOT NULL,
    [ApiKey]    NVARCHAR (100) NOT NULL,
    [SecretKey] NVARCHAR (100) NOT NULL,
    [BotStatus] INT            NOT NULL,
    [BotStatusMsg] NVARCHAR(1000) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Users_ApiKey] UNIQUE NONCLUSTERED ([ApiKey] ASC, [SecretKey] ASC)
);

