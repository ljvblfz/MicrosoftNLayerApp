ALTER TABLE [dbo].[BankAccount]
    ADD CONSTRAINT [FK_BankAccount_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

