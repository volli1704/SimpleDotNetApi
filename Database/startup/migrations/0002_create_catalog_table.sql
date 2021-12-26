USE Catalog

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Catalog' and xtype='U')
BEGIN
    CREATE TABLE Catalog (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name VARCHAR(255) NOT NULL,
        Price MONEY NOT NULL,
        CreatedDate datetime NOT NULL,
        UUID varchar(255) NOT NULL,
    )
END