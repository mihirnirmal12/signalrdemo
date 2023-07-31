using Microsoft.EntityFrameworkCore;
using signalr.Hubs;
using signalr.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dotnetinternalContext>(optionbuilder =>
{
    optionbuilder.UseSqlServer("Server=PC0420\\MSSQL2019;Database=dotnetinternal;Trusted_Connection=True;TrustServerCertificate=True;");
});


string name = "demo";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name,
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseCors("demo");

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatsocket");

});

app.Run();
