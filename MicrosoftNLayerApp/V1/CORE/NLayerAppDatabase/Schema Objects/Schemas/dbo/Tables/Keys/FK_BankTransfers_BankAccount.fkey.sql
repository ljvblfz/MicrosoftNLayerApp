ALTER TABLE [dbo].[BankTransfers]
    ADD CONSTRAINT [FK_BankTransfers_BankAccount] FOREIGN KEY ([FromBankAccountId]) REFERENCES [dbo].[BankAccount] ([BankAccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

