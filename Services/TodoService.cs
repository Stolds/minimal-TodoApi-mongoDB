
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    private readonly IMongoCollection<Todo> _todoCollection;

    public TodoService(IOptions<TodoApiDbSettings> todoApiDbSettings)
    {
        var mongoClient = new MongoClient(todoApiDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(todoApiDbSettings.Value.DatabaseName);
        _todoCollection = mongoDatabase.GetCollection<Todo>(todoApiDbSettings.Value.TodoCollectionName);
    }

    public async Task<List<Todo>> GetAsync() =>
        await _todoCollection.Find(_ => true).ToListAsync();

    public async Task<Todo?> GetAsync(string id) =>
        await _todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Todo newTodo) =>
        await _todoCollection.InsertOneAsync(newTodo);
    
    public async Task UpdateAsync(string id, Todo updatedTodo) => 
        await _todoCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);
    
    public async Task DeleteAsync(string id) =>
        await _todoCollection.DeleteOneAsync(x => x.Id == id);
}