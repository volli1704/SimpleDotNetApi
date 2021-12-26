IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Catalog')
BEGIN
  CREATE DATABASE Catalog
END