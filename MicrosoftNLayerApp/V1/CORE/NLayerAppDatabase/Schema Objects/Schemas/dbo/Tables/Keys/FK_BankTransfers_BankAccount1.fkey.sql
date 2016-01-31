ALTER TABLE [dbo].[BankTransfers]
    ADD CONSTRAINT [FK_BankTransfers_BankAccount1] FOREIGN KEY ([ToBankAccountId]) REFERENCES [dbo].[BankAccount] ([BankAccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

