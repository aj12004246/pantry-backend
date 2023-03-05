using Microsoft.EntityFrameworkCore;
using pantry_be.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<pantry_be.DataContext>(options =>
{
    options.UseSqlServer("Server=localhost;Initial Catalog=StoreDB;Integrated Security=False;User Id=sa;Password=Your_password123;MultipleActiveResultSets=True");
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IStepService, StepService>();
builder.Services.AddScoped<IFriendService, FriendService>();


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

app.UseCors("localhost");

app.Run();
