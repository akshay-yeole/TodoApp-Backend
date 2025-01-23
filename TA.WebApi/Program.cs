

using TA.Core.AppExceptions;
using TA.DataRepository;
using TA.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureBusinessService();
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDb<TodoAppContext>(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(_ => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
