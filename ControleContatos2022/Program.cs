/* Parte do Startup */
using ControleContatos2022.Data;
using ControleContatos2022.Helper;
using ControleContatos2022.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BancoContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("Database")));

//configurando o http usado na sessao do usuario
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

//Toda vez que o IContatoRepository for chamado o ContatoRepository vai ser chamado também
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//usado para configurar a sessao do usuario
builder.Services.AddSession(o => 
    {
        o.Cookie.HttpOnly = true;
        o.Cookie.IsEssential = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();//permitir o uso das sessoes

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
