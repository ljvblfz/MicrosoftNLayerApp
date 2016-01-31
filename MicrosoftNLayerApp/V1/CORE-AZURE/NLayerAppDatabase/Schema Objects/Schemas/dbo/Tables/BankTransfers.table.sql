CREATE TABLE [dbo].[BankTransfers] (
    [BankTransferId]    INT             IDENTITY (1, 1) NOT NULL,
    [FromBankAccountId] INT             NOT NULL,
    [ToBankAccountId]   INT             NOT NULL,
    [Amount]            NUMERIC (18, 2) NOT NULL,
    [TransferDate]      DATETIME        NOT NULL
);

