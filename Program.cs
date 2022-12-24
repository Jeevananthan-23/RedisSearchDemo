using Redis.OM;
using RedisSearchDemo.Helper;
using RedisSearchDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//redis connect
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration.GetValue<string>("ConnectionString:Redis")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
  Seeder.Seed(serviceScope).GetAwaiter();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
