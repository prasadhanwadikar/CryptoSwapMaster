﻿CREATE TABLE [dbo].[ExchangeOrders] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [OrderId]         INT             NOT NULL,
    [Sequence]        INT             NOT NULL,
    [ExchangeOrderId] VARCHAR (200)  NULL,
    [Symbol]          VARCHAR (10)   NOT NULL,
    [Side]            VARCHAR (10)   NOT NULL,
    [BaseQty]     DECIMAL (18, 8) NOT NULL,
    [QuoteQty]        DECIMAL (18, 8) NOT NULL,
    [Status]          INT             NOT NULL,
    [StatusMsg]       VARCHAR (1000) NULL,
    [Created]         DATETIME        NOT NULL,
    [LastModified]    DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExchangeOrders_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);

