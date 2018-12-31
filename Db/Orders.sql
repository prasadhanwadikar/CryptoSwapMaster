CREATE TABLE [dbo].[Orders] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [OrderGroup]    INT           NOT NULL,
    [BaseAsset]     NVARCHAR (10) NOT NULL,
    [QuoteAsset]    NVARCHAR (10) NOT NULL,
    [BaseQty]       FLOAT (53)    NOT NULL,
    [BaseUnitPrice] FLOAT (53)    NOT NULL,
    [Status]        INT           NOT NULL,
    [Created]       DATETIME      NOT NULL,
    [LastModified]  DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

