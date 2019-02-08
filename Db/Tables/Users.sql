CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Ip]           VARCHAR (25)   NOT NULL,
    [ApiKey]       VARCHAR (100)  NOT NULL,
    [SecretKey]    VARCHAR (100)  NOT NULL,
    [BotStatus]    INT             NOT NULL,
    [BotStatusMsg] VARCHAR (1000) NULL,
    [Created]      DATETIME        NOT NULL,
    [LastModified] DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Users_ApiKey] UNIQUE NONCLUSTERED ([ApiKey] ASC, [SecretKey] ASC)
);

