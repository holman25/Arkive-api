IF DB_ID('ArkiveDb') IS NULL
BEGIN
  PRINT 'Creating database ArkiveDb...';
  CREATE DATABASE ArkiveDb;
END
GO

USE ArkiveDb;
GO

IF OBJECT_ID('dbo.Documentos') IS NULL
BEGIN
  PRINT 'Creating table dbo.Documentos...';
  CREATE TABLE dbo.Documentos (
      Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
      Titulo NVARCHAR(255) NOT NULL,
      Autor NVARCHAR(255) NOT NULL,
      Tipo NVARCHAR(100) NOT NULL,
      Estado NVARCHAR(100) NOT NULL,
      FechaRegistro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
  );
END
ELSE
BEGIN
  PRINT 'Table dbo.Documentos already exists. Skipping.';
END
GO
