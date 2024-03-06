using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;

using UserLogin.Data;
using UserLogin.Helpers;
using UserLogin.Helpers.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container..
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var folder = Path.Combine(Directory.GetCurrentDirectory(), "Db_Sqlite");
Directory.CreateDirectory(folder);
var connection_Accounts = $"Data Source={Path.Combine(folder, "AccountsContext.db")}";
builder.Services.AddDbContext<UserContext>(options => options.UseSqlite(connection_Accounts));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options => options
    .WithOrigins(new[] { "http://localhost:3000", "http://localhost:4200" })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
/*
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<UserContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}
*/

app.UseAuthorization();

app.MapControllers();

app.Run();
