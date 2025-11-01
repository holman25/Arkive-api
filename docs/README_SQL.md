
#  Scripts de Base de Datos — Arkive

Este archivo explica cómo ejecutar y utilizar los scripts SQL del proyecto **ArkiveDb**,
parte del sistema de gestión documental *Arkive Document Management System*.

---

##  Archivos Incluidos

| Archivo | Descripción |
|----------|--------------|
| `database_setup.sql` | Crea la base de datos **ArkiveDb** y la tabla `Documentos` si no existen. |
| `seed_data.sql` | Inserta 20 registros de prueba (solo si la tabla está vacía). |
| `queries_optimizadas.sql` | Crea un procedimiento almacenado, índices y consultas optimizadas. |

---

##  Instrucciones de Ejecución

1. **Abrir SQL Server Management Studio (SSMS)** y conectarse a tu servidor local.  
2. Ejecutar los archivos **en este orden**:

   ```sql
   -- Paso 1: Crear base de datos y tabla
   :r database_setup.sql

   -- Paso 2: Insertar datos de prueba
   :r seed_data.sql

   -- Paso 3: Crear procedimientos e índices
   :r queries_optimizadas.sql
   ```

3. **Verificar los datos cargados:**
   ```sql
   USE ArkiveDb;
   SELECT TOP 5 * FROM dbo.Documentos;
   ```

4. **Probar el procedimiento almacenado:**
   ```sql
   EXEC dbo.SP_ReportePorAutorYTipo;
   ```

---

##  Notas Importantes

- Los scripts son **seguros e idempotentes**, es decir, puedes ejecutarlos varias veces sin riesgo de duplicar datos o romper la estructura.  
- Si la tabla `Documentos` ya contiene registros, el script de datos (`seed_data.sql`) omitirá la inserción.  
- El nombre de la base de datos usada es **ArkiveDb**, el mismo configurado en la cadena de conexión de Entity Framework Core.  
- Estos archivos deben ubicarse dentro de la carpeta `docs/` del repositorio.

---

📘 **Autor:** Holman Alba  
