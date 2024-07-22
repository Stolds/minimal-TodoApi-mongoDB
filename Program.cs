using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<TodoApiDbSettings>(
    builder.Configuration.GetSection("TodoApiDatabase"));

builder.Services.AddSingleton<TodoService>();

app.MapGet("/todo", async (TodoService db) => await db.GetAsync());

app.MapGet("/", () => "Hello World!");

app.Run();