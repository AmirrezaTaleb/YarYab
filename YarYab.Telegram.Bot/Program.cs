using Microsoft.Extensions.Options;
using Telegram.Bot;
using YarYab.Telegram.Bot;
using YarYab.Telegram.Bot.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register Bot configuration
        services.Configure<BotConfiguration>(context.Configuration.GetSection("BotConfiguration"));

            services.AddHttpClient(Environment.GetEnvironmentVariable("TOKEN"))
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                     TelegramBotClientOptions options = new(Environment.GetEnvironmentVariable("TOKEN"));
                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();
