using Consumer.API.ConsumeInterceptors;
using Shared;
using Shared.Messages;
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
        .ScanConsumersFromAssemblies(typeof(Program).Assembly)
        //.UseRabbitMq();
        //.UseKafka()
        .UseAzureServiceBus();
})
.AddConsumeHandler<HelloWorldMessage>(Topics.HelloWorld, (ctx, message) =>
{
    var logger = ctx.Services.GetService<ILogger<HelloWorldMessage>>()!;
    logger.LogInformation(message.Text);

    return Task.CompletedTask;
})
.AddConsumeInterceptor<TraceabilityConsumeInterceptor>();

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
