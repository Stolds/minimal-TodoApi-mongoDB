using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<TodoApiDbSettings>(
    builder.Configuration.GetSection("TodoApiDatabase"));




app.MapGet("/", () => "Hello World!");

app.Run();