using BlockchainApp.Db;
using BlockchainApp.Services.MiningServ;
using BlockchainApp.Services.TransactionServ;
using BlockchainApp.Services.UserServ;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BlockchainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMiningService, MiningService>();


builder.Services.AddControllers()
     .AddNewtonsoftJson(opt =>
     {
         opt.SerializerSettings.ContractResolver = new DefaultContractResolver();

         opt.SerializerSettings.ReferenceLoopHandling =
         Newtonsoft.Json.ReferenceLoopHandling.Ignore;
     });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
