CREATE TABLE [dbo].[QuoteAssets] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [UserId] INT           NOT NULL,
    [Asset]  VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuoteAssets_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

