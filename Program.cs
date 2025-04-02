using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços necessários
builder.Services.AddControllers();

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Definindo as informações da API no Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task API",
        Version = "v1",
        Description = "Uma API simples para gerenciar tarefas"
    });
});

var app = builder.Build();

// Habilitar o Swagger e Swagger UI apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o arquivo Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
        c.RoutePrefix = string.Empty;  // Torna o Swagger acessível diretamente na raiz
    });
}

app.MapControllers(); // Mapeia os controladores

app.Run();
