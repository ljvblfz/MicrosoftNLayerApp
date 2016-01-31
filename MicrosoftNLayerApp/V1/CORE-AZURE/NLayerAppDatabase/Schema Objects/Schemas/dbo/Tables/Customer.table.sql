CREATE TABLE [dbo].[Customer] (
    [CustomerId]   INT           IDENTITY (1, 1) NOT NULL,
    [CustomerCode] NVARCHAR (5)  COLLATE Modern_Spanish_CI_AS NOT NULL,
    [CompanyName]  NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
    [ContactName]  NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NULL,
    [ContactTitle] NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NULL,
    [Address]      NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NULL,
    [City]         NVARCHAR (20) COLLATE Modern_Spanish_CI_AS NULL,
    [PostalCode]   NVARCHAR (10) COLLATE Modern_Spanish_CI_AS NULL,
    [Telephone]    NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NULL,
    [Fax]          NVARCHAR (50) COLLATE Modern_Spanish_CI_AS NULL,
    [CountryId]    INT           NULL,
    [Photo]        IMAGE         NULL,
    [IsEnabled]    BIT           NOT NULL
);



