// Test comment: AI assistant is working correctly.
var builder = WebApplication.CreateBuilder(args);

// 1. დაამატეთ Swagger სერვისები
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. ჩართეთ Swagger-ის Middleware Development გარემოში
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapGet("/test/data", () => Results.Ok("hello new endpoint"));

app.Run();
