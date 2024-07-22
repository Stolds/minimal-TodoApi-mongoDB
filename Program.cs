using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<TodoApiDbSettings>(
    builder.Configuration.GetSection("TodoApiDatabase"));

builder.Services.AddSingleton<TodoService>();
var app = builder.Build();


app.MapGet("/todo", async (TodoService db) => await db.GetAsync());

app.MapGet("/", () => "Hello World!");

app.Run();