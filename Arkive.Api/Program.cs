using Microsoft.EntityFrameworkCore;
using Arkive.Application.Abstractions;
using Arkive.Infrastructure.Repositories;
using Arkive.Application.Services;
using Arkive.Infrastructure.Config;
using Arkive.Infrastructure.Jobs;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Arkive.Infrastructure.Persistence.AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
builder.Services.Configure<ArchivadorSettings>(
    builder.Configuration.GetSection("Archivador"));

builder.Services.AddHostedService<ArchivadorHostedService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
public partial class Program { }
