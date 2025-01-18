

using TA.Core.AppExceptions;
using TA.DataRepository;
using TA.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Business Services
builder.Services.ConfigureBusinessService();

// Exception Handler
builder.Services.AddExceptionHandler<AppExceptionHandler>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurations
builder.Services.ConfigureDb<TodoAppContext>(builder.Configuration);

var app = builder.Build();

//Exception Middleware
app.UseExceptionHandler(_ => { });

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
