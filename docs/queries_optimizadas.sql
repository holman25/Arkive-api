USE ArkiveDb;
GO

-- Stored Procedure: SP_ReportePorAutorYTipo
IF OBJECT_ID('dbo.SP_ReportePorAutorYTipo', 'P') IS NULL
    EXEC ('CREATE PROCEDURE dbo.SP_ReportePorAutorYTipo AS SET NOCOUNT ON;');
GO

ALTER PROCEDURE dbo.SP_ReportePorAutorYTipo
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Autor, Tipo, COUNT(*) AS Cantidad
    FROM dbo.Documentos
    GROUP BY Autor, Tipo
    ORDER BY Autor, Tipo;

    SELECT COUNT(*) AS TotalGeneral FROM dbo.Documentos;
END
GO

SELECT 
    Tipo,
    AVG(DATEDIFF(
        DAY,
        MIN(CASE WHEN Estado = N'Registrado' THEN FechaRegistro END),
        MAX(CASE WHEN Estado = N'Validado' THEN FechaRegistro END)
    )) AS PromedioDias
FROM dbo.Documentos
GROUP BY Tipo;
GO

-- Create indexes only if not exists
IF NOT EXISTS (
  SELECT 1 FROM sys.indexes WHERE name = 'IX_Documentos_Autor' AND object_id = OBJECT_ID('dbo.Documentos')
)
  CREATE INDEX IX_Documentos_Autor ON dbo.Documentos(Autor);

IF NOT EXISTS (
  SELECT 1 FROM sys.indexes WHERE name = 'IX_Documentos_Tipo' AND object_id = OBJECT_ID('dbo.Documentos')
)
  CREATE INDEX IX_Documentos_Tipo ON dbo.Documentos(Tipo);

IF NOT EXISTS (
  SELECT 1 FROM sys.indexes WHERE name = 'IX_Documentos_Estado' AND object_id = OBJECT_ID('dbo.Documentos')
)
  CREATE INDEX IX_Documentos_Estado ON dbo.Documentos(Estado);
GO
