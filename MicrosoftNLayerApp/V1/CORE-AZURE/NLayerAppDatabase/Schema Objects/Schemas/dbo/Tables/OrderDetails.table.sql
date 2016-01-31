CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailsId] INT      IDENTITY (1, 1) NOT NULL,
    [OrderId]        INT      NOT NULL,
    [ProductId]      INT      NOT NULL,
    [UnitPrice]      MONEY    NULL,
    [Amount]         SMALLINT NULL,
    [Discount]       REAL     NULL
);

