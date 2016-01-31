
use NLayerApp;

DELETE  FROM [BankTransfers];
DELETE  FROM [BankAccount];
DELETE  FROM [Book];
DELETE FROM [Software];
DELETE FROM [OrderDetails];
DELETE FROM [Order];
DELETE FROM [Product];
DELETE FROM [Customer];
DELETE FROM [Country];
GO

--RESSED IDENTITY COLUMNS

DBCC CHECKIDENT('[BankTransfers]',reseed,0)
GO
DBCC CHECKIDENT('[BankAccount]',reseed,0)
GO
DBCC CHECKIDENT('[OrderDetails]',reseed,0)
GO
DBCC CHECKIDENT('[Order]',reseed,0)
GO
DBCC CHECKIDENT('[Customer]',reseed,0)
GO
DBCC CHECKIDENT('[Country]',reseed,0)
GO
DBCC CHECKIDENT('[Product]',reseed,0)
GO





--Insert database records for country entity


INSERT INTO dbo.Country values('Spain');
INSERT INTO dbo.Country values('United States');
INSERT INTO dbo.Country values('Germany');
INSERT INTO dbo.Country values('France');
INSERT INTO dbo.Country values('Sweeden');
INSERT INTO dbo.Country values('Norway');
INSERT INTO dbo.Country values('Italy');
INSERT INTO dbo.Country values('Brazil');
INSERT INTO dbo.Country values('Argentina');
INSERT INTO dbo.Country values('Russia');
INSERT INTO dbo.Country values('China');


-- Insert database records for customer entity
INSERT INTO [dbo].Customer Values('A0001','Microsoft','Cesar De la Torre','Architect Evangelist','Parque Empresarial de La Finca','Madrid','28081','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0002','Plain Concepts ','Unai Zorrilla','Developer Team Leader','Sebastian el Cano','Madrid','28081','+34981555000','+34981555001',1,null,1);
INSERT INTO [dbo].Customer Values('A0003','Microsoft','Miguel Angel Ramos','Premier TAM','Parque Empresarial de La Finca','Madrid','28081','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0004','Microsoft','Pierre Milet','Senior Consultant','Parque Empresarial de La Finca','Madrid','28081','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0005','Microsoft','Ricardo Minguez (Rido)','Senior Consultant','Parque Empresarial de La Finca','Madrid','28081','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0006','JetBrains','Hadi Hariri','Technology Evangelist','Calle Development','Malaga','28123','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0007','Renacimiento','Roberto Gonzalez','Software Architect','Calle del Arte 21','Madrid','28123','+34981666666','+34981666666',1,null,1);
INSERT INTO [dbo].Customer Values('A0008','Plain Concepts ','Fernando Cortes Hierro','Developer Advisor','Sebastian el Cano','Madrid','28081','+34981555000','+34981555001',1,null,1);
INSERT INTO [dbo].Customer Values('A0009','Hollywood','Megan Fox','Actress','Star Street','L.A.','28999','+319999999','+319999999',1,null,1);

-- Insert database records for product entity
INSERT INTO dbo.Product VALUES('ADO.NET Entity Framework',40,1,'Krasis Press',20);
INSERT INTO dbo.Product VALUES('Windows 7',200,1,'Microsoft Corp',200);
INSERT INTO dbo.Book VALUES(1,'Krasis Press');
INSERT INTO dbo.Software VALUES(2,'ABCX-0002-33333-66666');
INSERT INTO dbo.BankAccount VALUES('BAC0000001',100,1,0);
INSERT INTO dbo.BankAccount VALUES('BAC0000002',200,2,0);
INSERT INTO dbo.BankAccount VALUES('BAC0000003',200,2,1);
INSERT INTO dbo.BankTransfers VALUES(1,2,50,'2010-1-1');
INSERT INTO dbo.BankTransfers VALUES(2,1,35,'2010-5-1');
INSERT INTO dbo.[Order] VALUES(1,'2010-1-1',DATEADD(day,1,'2010-1-1'),'Book EF buy','Sebastian el Cano','Madrid','28081');
INSERT INTO dbo.[Order] VALUES(1,'2010-5-1',DATEADD(day,5,'2010-5-1'),'Windows Seven buy','Sebastian el Cano','Madrid','28081');
INSERT INTO dbo.[OrderDetails] VALUES(1,1,40,20,0);
INSERT INTO dbo.[OrderDetails] VALUES(1,2,200,20,0);




