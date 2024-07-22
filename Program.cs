using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<TodoApiDbSettings>(
    builder.Configuration.GetSection("TodoApiDatabase"));

builder.Services.AddSingleton<TodoService>();
var app = builder.Build();


app.MapGet("/todo", async (TodoService db) => await db.GetAsync());
app.MapGet("/todo/{id}", async (TodoService db, string id) => await db.GetAsync(id));

app.MapPost("/todo", async (TodoService db, Todo todo) =>
{
    await db.CreateAsync(todo);
    return Results.Created($"/todo/{todo.Id}", todo);
});

app.MapPut("/todo/{id}", async (TodoService db, Todo updatedTodo, string id) =>
{
    var todo = await db.GetAsync(id);
    if(todo is null) return Results.NotFound("don't was found any task with this id");
    todo.Name = updatedTodo.Name;
    todo.Description = updatedTodo.Description;
    todo.Done = updatedTodo.Done;
    todo.ScheduledTime = updatedTodo.ScheduledTime;
    await db.UpdateAsync(id, updatedTodo);
    return Results.NoContent(); 
});

app.MapDelete("/todo/{id}", async (TodoService db, string id) => 
{
    var todo = await db.GetAsync(id);
    if(todo is null) return Results.NotFound("task not found");
    await db.DeleteAsync(id);
    return Results.Ok();
});


app.MapGet("/", () => "Hello World!");

app.Run();