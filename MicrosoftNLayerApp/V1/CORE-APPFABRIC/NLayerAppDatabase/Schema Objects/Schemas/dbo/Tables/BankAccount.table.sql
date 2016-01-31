CREATE TABLE [dbo].[BankAccount] (
    [BankAccountId]     INT           IDENTITY (1, 1) NOT NULL,
    [BankAccountNumber] NVARCHAR (10) COLLATE Modern_Spanish_CI_AI NOT NULL,
    [Balance]           MONEY         NOT NULL,
    [CustomerId]        INT           NULL,
    [Locked]            BIT           NOT NULL
);

