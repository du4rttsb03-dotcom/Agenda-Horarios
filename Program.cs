using AutoEscolaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoEscolaAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. ADICIONAR SERVIÇOS
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 2. CONFIGURAR CORS (Para o frontend falar com o backend)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("PermitirTudo",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
        });

        // Configure o banco de dados
        builder.Services.AddDbContext<AutoEscolaContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // 3. CONFIGURAR O PIPELINE HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // 4. ATIVAR CORS (Importante: Antes do Authorization)
        app.UseCors("PermitirTudo");

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}