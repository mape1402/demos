using Producer.API.PublishInterceptors;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPigeon(builder.Configuration, settings =>
{
    settings
        //.UseRabbitMq();
        //.UseKafka();
        //.UseAzureServiceBus();
        .UseAzureEventGrid();
})
.AddPublishInterceptor<TraceabilityPublishInterceptor>();

builder.Services.AddScoped<ITraceabilityContext, TraceabilityContext>();

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
