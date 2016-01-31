ALTER TABLE [dbo].[Book]
    ADD CONSTRAINT [FK_Book_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

