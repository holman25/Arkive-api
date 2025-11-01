USE ArkiveDb;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name='Documentos' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    RAISERROR('Table dbo.Documentos does not exist. Run database_setup.sql first.', 16, 1);
    RETURN;
END

IF NOT EXISTS (SELECT 1 FROM dbo.Documentos)
BEGIN
  PRINT 'Seeding 20 sample rows into dbo.Documentos...';
  INSERT INTO dbo.Documentos (Titulo, Autor, Tipo, Estado, FechaRegistro) VALUES
  (N'Informe inicial', N'Holman Alba', N'Informe', N'Registrado', SYSUTCDATETIME()),
  (N'Contrato de mantenimiento', N'Laura Pérez', N'Contrato', N'Pendiente', DATEADD(DAY,-15,SYSUTCDATETIME())),
  (N'Acta de reunión 01', N'Carlos Gómez', N'Acta', N'Validado', DATEADD(DAY,-35,SYSUTCDATETIME())),
  (N'Manual de procesos', N'Andrea Torres', N'Informe', N'Registrado', SYSUTCDATETIME()),
  (N'Informe de validación', N'Holman Alba', N'Informe', N'Validado', DATEADD(DAY,-5,SYSUTCDATETIME())),
  (N'Contrato proveedor A', N'María Rojas', N'Contrato', N'Archivado', DATEADD(DAY,-120,SYSUTCDATETIME())),
  (N'Acta comité técnico', N'Luis Herrera', N'Acta', N'Pendiente', DATEADD(DAY,-10,SYSUTCDATETIME())),
  (N'Informe de resultados', N'Holman Alba', N'Informe', N'Registrado', DATEADD(DAY,-60,SYSUTCDATETIME())),
  (N'Carta compromiso', N'Paula Díaz', N'Acta', N'Validado', SYSUTCDATETIME()),
  (N'Informe final', N'Jeyner Escarraga', N'Informe', N'Registrado', SYSUTCDATETIME()),
  (N'Contrato proveedor B', N'María Rojas', N'Contrato', N'Pendiente', DATEADD(DAY,-25,SYSUTCDATETIME())),
  (N'Informe de calidad', N'Holman Alba', N'Informe', N'Validado', DATEADD(DAY,-40,SYSUTCDATETIME())),
  (N'Acta de entrega', N'Andrés Ruiz', N'Acta', N'Archivado', DATEADD(DAY,-95,SYSUTCDATETIME())),
  (N'Procedimiento interno', N'Laura Pérez', N'Informe', N'Pendiente', SYSUTCDATETIME()),
  (N'Contrato de servicios', N'Juan Castillo', N'Contrato', N'Registrado', SYSUTCDATETIME()),
  (N'Informe de pruebas', N'Holman Alba', N'Informe', N'Registrado', SYSUTCDATETIME()),
  (N'Acta de cierre', N'Carlos Gómez', N'Acta', N'Validado', DATEADD(DAY,-70,SYSUTCDATETIME())),
  (N'Documento de control', N'Andrea Torres', N'Informe', N'Registrado', DATEADD(DAY,-10,SYSUTCDATETIME())),
  (N'Contrato especial', N'Paula Díaz', N'Contrato', N'Registrado', SYSUTCDATETIME()),
  (N'Reporte interno', N'Holman Alba', N'Informe', N'Pendiente', DATEADD(DAY,-3,SYSUTCDATETIME()));
END
ELSE
BEGIN
  PRINT 'dbo.Documentos already has data. Skipping seeds.';
END
GO
