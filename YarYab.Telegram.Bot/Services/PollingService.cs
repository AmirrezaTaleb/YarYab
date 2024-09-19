using Microsoft.Extensions.Logging;
using YarYab.Telegram.Bot.Abstract;

namespace YarYab.Telegram.Bot.Services;

// Compose Polling and ReceiverService implementations
public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);
