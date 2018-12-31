CREATE TABLE [dbo].[ExchangeOrders] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [OrderId]         INT            NOT NULL,
    [ExchangeOrderId] NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

