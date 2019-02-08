CREATE TABLE [dbo].[Orders] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [UserId]           INT             NOT NULL,
    [BaseAsset]        VARCHAR (10)   NOT NULL,
    [Pool]             INT             NOT NULL,
    [Type]		VARCHAR(20)  NOT NULL,
    [BaseQty]          DECIMAL (18, 8) NOT NULL,
    [ExecutedBaseQty]	DECIMAL (18, 8) NULL,
    [QuoteAsset]       VARCHAR (10)   NOT NULL,
    [ExpectedQuoteQty] DECIMAL (18, 8) NOT NULL,
    [ReceivedQuoteQty] DECIMAL (18, 8) NULL,
    [Status]           INT             NOT NULL,
    [StatusMsg]        VARCHAR (1000) NULL,
    [Created]          DATETIME        NOT NULL,
    [LastModified]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

