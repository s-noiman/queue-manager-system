using Microsoft.EntityFrameworkCore;
using QueueManager;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<QueueContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Queue_db")));
builder.Services.AddCors(p => p.AddPolicy("corsPolicy", x => { x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddControllers(options => options.EnableEndpointRouting = false);

var app = builder.Build();

app.MapGet("/", () => "Welcome To the queue manager server :)\nEnjoy it ❤️");

// Get all queues.
app.MapGet("/api/queues", async (QueueContext db) => await db.Queues.ToListAsync());

// Get queue by ID.
app.MapGet("/api/queues/{id}", async (QueueContext db, int id) => await db.Queues.FindAsync(id));

// Add new queue.
app.MapPost("/api/queues", async (QueueContext db, Queue queue) => 
{
    await db.Queues.AddAsync(queue);
    await db.SaveChangesAsync();
    Results.Accepted();
});

// Update queue by ID.
app.MapPut("/api/queues/{id}", async (QueueContext db, int id, Queue queue) => 
{
    if (id != queue.Id) return Results.BadRequest();

    db.Queues.Update(queue);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Delete queue by ID.
app.MapDelete("/api/queues/{id}", async (QueueContext db, int id) => 
{
    var queue = await db.Queues.FindAsync(id);
    if (queue == null) return Results.NotFound();

    db.Queues.Remove(queue);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.UseCors("corsPolicy");
app.UseHttpsRedirection(); app.Run();
