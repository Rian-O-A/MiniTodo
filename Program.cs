using MiniTodo.Data;
using MiniTodo.Models;
using MiniTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbCotenxt>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbCotenxt cotenxt) =>
{
    // var todo = new Todo(Guid.NewGuid(), "Ir a academia", false);
    var todos = cotenxt.Todos.ToList();
 
    return Results.Ok(todos);
}).Produces<Todo>();


app.MapGet("v1/todos/{id}", (AppDbCotenxt context, Guid id) =>
{
    // Agora, o parâmetro 'id' será capturado da URL
    var todo = context.Todos.FirstOrDefault(t => t.Id == id);

    if (todo == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(todo);

}).Produces<Todo>();


app.MapPost("v1/todos", (
    AppDbCotenxt context,
    CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

app.Run();
