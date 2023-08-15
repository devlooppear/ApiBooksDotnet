var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve MongoDB connection details from appsettings.json
string connectionString = builder.Configuration["MongoDB:ConnectionString"];
string databaseName = builder.Configuration["MongoDB:DatabaseName"];

// Register the BooksContext
builder.Services.AddSingleton<BooksContext>(sp => new BooksContext(connectionString, databaseName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
