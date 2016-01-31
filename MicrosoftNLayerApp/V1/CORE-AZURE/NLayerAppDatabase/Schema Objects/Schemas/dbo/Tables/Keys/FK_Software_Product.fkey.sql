ALTER TABLE [dbo].[Software]
    ADD CONSTRAINT [FK_Software_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

