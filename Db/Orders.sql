CREATE TABLE [dbo].[Orders] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [UserId]       INT             NOT NULL,
    [BaseAsset]    NVARCHAR (10)   NOT NULL,
    [Group]        INT             NOT NULL,
    [SubGroup]     INT             NOT NULL,
    [BaseQty]      FLOAT (53)      NOT NULL,
    [QuoteAsset]   NVARCHAR (10)   NOT NULL,
    [ExpectedQuoteQty]     FLOAT (53)      NOT NULL,
	[ReceivedQuoteQty]     FLOAT (53)      NULL,
    [Status]       INT             NOT NULL,
    [StatusMsg]    NVARCHAR (1000) NULL,
    [Created]      DATETIME        NOT NULL,
    [LastModified] DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

