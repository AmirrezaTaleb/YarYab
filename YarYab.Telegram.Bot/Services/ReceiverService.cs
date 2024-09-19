using YarYab.Telegram.Bot.Abstract;
using Telegram.Bot;
using Microsoft.Extensions.Logging;

namespace YarYab.Telegram.Bot.Services;

// Compose Receiver and UpdateHandler implementation
public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler>> logger)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger);
