CREATE TABLE [dbo].[Product] (
    [ProductId]          INT            IDENTITY (1, 1) NOT NULL,
    [ProductDescription] NVARCHAR (100) COLLATE Modern_Spanish_CI_AS NULL,
    [UnitPrice]          MONEY          NULL,
    [UnitAmount]         NVARCHAR (50)  COLLATE Modern_Spanish_CI_AS NULL,
    [Publisher]          NVARCHAR (200) COLLATE Modern_Spanish_CI_AS NULL,
    [AmountInStock]      SMALLINT       NULL
);

