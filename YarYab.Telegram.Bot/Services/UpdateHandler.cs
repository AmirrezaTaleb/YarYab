using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using YarYab.Telegram.Bot.Models;
using static Telegram.Bot.TelegramBotClient;

namespace YarYab.Telegram.Bot.Services;

public class UpdateHandler : IUpdateHandler
{
    private static readonly Dictionary<long, string> _userStates = new Dictionary<long, string>();
    private static readonly InputPollOption[] PollOptions = ["Hello", "World!"];
    private readonly IUserService _userService;
    private readonly ITelegramBotClient _bot;
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger, IUserService userService)
    {
        _bot = bot;
        _logger = logger;
        _userService = userService;
    }


    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HandleError: {Exception}", exception);
        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await (update switch
        {
            { Message: { } message } => OnMessage(message),
            { EditedMessage: { } message } => OnMessage(message),
            { CallbackQuery: { } callbackQuery } => OnCallbackQuery(callbackQuery),
            { InlineQuery: { } inlineQuery } => OnInlineQuery(inlineQuery),
            { ChosenInlineResult: { } chosenInlineResult } => OnChosenInlineResult(chosenInlineResult),
            { Poll: { } poll } => OnPoll(poll),
            { PollAnswer: { } pollAnswer } => OnPollAnswer(pollAnswer),
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            _ => UnknownUpdateHandlerAsync(update)
        });
    }
    public ReplyKeyboardMarkup HomeInlineKeyboardMarkup()
    {

        return new ReplyKeyboardMarkup(new[]
    {
        new[] { new KeyboardButton("🔗 به یه ناشناس وصلم کن!") },
        new[] { new KeyboardButton("🔍 جستجوی کاربران"), new KeyboardButton("📍 افراد نزدیک") },
        new[] { new KeyboardButton("💰 سکه"), new KeyboardButton("👤 پروفایل"), new KeyboardButton("❓ راهنما") },
        new[] { new KeyboardButton("🎁 معرفی به دوستان (سکه رایگان)") }
    })
        {
            ResizeKeyboard = true, // Optional: Resize the keyboard
            OneTimeKeyboard = true // Optional: Close keyboard after one use
        };
    }
    private async Task<Message> BackToHome(Message msg)
    {
        _userStates[msg.From.Id] = "BackToHome";

        string text = "از منوی پایین انتخاب کن !";
        var ReplyMarkup = HomeInlineKeyboardMarkup();
        return await _bot.SendTextMessageAsync(msg.Chat, text, replyMarkup: ReplyMarkup);

    }
    private async Task<bool> ValidMessage(Message msg)
    {
        if (msg.Type == MessageType.Location && _userStates.TryGetValue(key: msg.From.Id, out string state) && state == "awaiting_Sendlocation")
        {
            await _userService.SetLocation(userLocation: new UserLocationModel(userId: 3, lat: msg.Location.Latitude, lang: msg.Location.Longitude));
            await _bot.SendTextMessageAsync(msg.Chat, "لوکیشن با موفقیت تغییر کرد");
            await BackToHome(msg);
            return true;
        }
        switch (msg.Text)
        {
            case "🔗 به یه ناشناس وصلم کن!":
                {
                    return true;

                 };
            case "🔍 جستجوی کاربران":
                {
                    return true;

                 };
            case "💰 سکه":
                {
                    return true;

                 };
            case "🎁 معرفی به دوستان (سکه رایگان)":
                {
                    return true;

                 };
            case "👤 پروفایل":
                {
                    var profileModel = await _userService.ShowProfile(msg.Chat);
                    await _bot.SendPhotoAsync(msg.Chat, profileModel.Photo, caption: profileModel.Banner, replyMarkup: profileModel.InlineKeyboardMarkup);
                    return true;

                 };
            case "❓ راهنما":
                {
                    break;
                };

            default:
                break;
        }
        return false;
    }
    private async Task OnMessage(Message msg)
    {
        _logger.LogInformation("Receive message type: {MessageType}", msg.Type);
        if (await ValidMessage(msg))
            return;
        if (msg.Text is not { } messageText)
            return;

        Message sentMessage = await BackToHome(msg);
        //Message sentMessage = await (messageText.Split(' ')[0] switch
        //{
        //    "/user_profile" => _userService.ShowProfile(msg),
        //    "/photo" => SendPhoto(msg),
        //    "/inline_buttons" => SendInlineKeyboard(msg),
        //    "/keyboard" => SendReplyKeyboard(msg),
        //    "/remove" => RemoveKeyboard(msg),
        //    "/request" => RequestContactAndLocation(msg),
        //    "/inline_mode" => StartInlineQuery(msg),
        //    "/poll" => SendPoll(msg),
        //    "/poll_anonymous" => SendAnonymousPoll(msg),
        //    "/throw" => FailingHandler(msg),
        //    _ => Usage(msg)
        //});
        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);
    }
    private async Task OnCallbackQuery(CallbackQuery callbackQuery)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);
        switch (callbackQuery.Data)
        {
            case "my_location":
                {
                    var userLocation = await _userService.CurentLocation("7894");
                    await _bot.SendLocationAsync(chatId: callbackQuery.Message!.Chat, latitude: userLocation.Lat, longitude: userLocation.Lang, replyMarkup: _userService.LocationInlineKeyboardMarkup());
                    break;
                };
            case "change_or_set_my_location":
                {
                    await _bot.SendTextMessageAsync(chatId: callbackQuery.Message!.Chat, "لطفا از فعال بودن GPS خود مطمئن باشید .", replyMarkup: _userService.SetLocaionReplyKeyboardMarkup());
                    _userStates[callbackQuery.From.Id] = "awaiting_Sendlocation";
                    break;
                };
             default:
                await _bot.AnswerCallbackQueryAsync(callbackQuery.Id, $"Received {callbackQuery.Data}");
                await _bot.SendTextMessageAsync(callbackQuery.Message!.Chat, $"Received {callbackQuery.Data}");
                break;
        }
    }

    private Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    #region lib Helpers
    async Task<Message> Usage(Message msg)
    {
        const string usage = """
                <b><u>Bot menu</u></b>:
                /photo          - send a photo
                /user_profile          - send user profile
                /inline_buttons - send inline buttons
                /keyboard       - send keyboard buttons
                /remove         - remove keyboard buttons
                /request        - request location or contact
                /inline_mode    - send inline-mode results list
                /poll           - send a poll
                /poll_anonymous - send an anonymous poll
                /throw          - what happens if handler fails
            """;
        return await _bot.SendTextMessageAsync(msg.Chat, usage, parseMode: ParseMode.Html, replyMarkup: new ReplyKeyboardRemove());
    }

    async Task<Message> SendPhoto(Message msg)
    {
        await _bot.SendChatActionAsync(msg.Chat, ChatAction.UploadPhoto);
        await Task.Delay(2000); // simulate a long task
        await using var fileStream = new FileStream("Files/_bot.gif", FileMode.Open, FileAccess.Read);
        return await _bot.SendPhotoAsync(msg.Chat, fileStream, caption: "Read https://telegrambots.github.io/book/");
    }

     async Task<Message> SendInlineKeyboard(Message msg)
    {
        var inlineMarkup = new InlineKeyboardMarkup()
            .AddNewRow("1.1", "1.2", "1.3")
            .AddNewRow()
                .AddButton("WithCallbackData", "CallbackData")
                .AddButton(InlineKeyboardButton.WithUrl("WithUrl", "https://github.com/TelegramBots/Telegram.Bot"));
        return await _bot.SendTextMessageAsync(msg.Chat, "Inline buttons:", replyMarkup: inlineMarkup);
    }

    async Task<Message> SendReplyKeyboard(Message msg)
    {
        var replyMarkup = new ReplyKeyboardMarkup(true)
            .AddNewRow("1.1", "1.2", "1.3")
            .AddNewRow().AddButton("2.1").AddButton("2.2");
        return await _bot.SendTextMessageAsync(msg.Chat, "Keyboard buttons:", replyMarkup: replyMarkup);
    }

    async Task<Message> RemoveKeyboard(Message msg)
    {
        return await _bot.SendTextMessageAsync(msg.Chat, "Removing keyboard", replyMarkup: new ReplyKeyboardRemove());
    }

    async Task<Message> RequestContactAndLocation(Message msg)
    {
        var replyMarkup = new ReplyKeyboardMarkup(true)
            .AddButton(KeyboardButton.WithRequestLocation("Location"))
            .AddButton(KeyboardButton.WithRequestContact("Contact"));
        return await _bot.SendTextMessageAsync(msg.Chat, "Who or Where are you?", replyMarkup: replyMarkup);
    }

    async Task<Message> StartInlineQuery(Message msg)
    {
        var button = InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Inline Mode");
        return await _bot.SendTextMessageAsync(msg.Chat, "Press the button to start Inline Query\n\n" +
            "(Make sure you enabled Inline Mode in @BotFather)", replyMarkup: new InlineKeyboardMarkup(button));
    }

    async Task<Message> SendPoll(Message msg)
    {
        return await _bot.SendPollAsync(msg.Chat, "Question", PollOptions, isAnonymous: false);
    }

    async Task<Message> SendAnonymousPoll(Message msg)
    {
        return await _bot.SendPollAsync(chatId: msg.Chat, "Question", PollOptions);
    }

    static Task<Message> FailingHandler(Message msg)
    {
        throw new NotImplementedException("FailingHandler");
    }

    #region Inline Mode

    private async Task OnInlineQuery(InlineQuery inlineQuery)
    {
        _logger.LogInformation("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

        InlineQueryResult[] results = [ // displayed result
            new InlineQueryResultArticle("1", "Telegram.Bot", new InputTextMessageContent("hello")),
            new InlineQueryResultArticle("2", "is the best", new InputTextMessageContent("world"))
        ];
        await _bot.AnswerInlineQueryAsync(inlineQuery.Id, results, cacheTime: 0, isPersonal: true);
    }

    private async Task OnChosenInlineResult(ChosenInlineResult chosenInlineResult)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);
        await _bot.SendTextMessageAsync(chosenInlineResult.From.Id, $"You chose result with Id: {chosenInlineResult.ResultId}");
    }

    #endregion

    private Task OnPoll(Poll poll)
    {
        _logger.LogInformation("Received Poll info: {Question}", poll.Question);
        return Task.CompletedTask;
    }

    private async Task OnPollAnswer(PollAnswer pollAnswer)
    {
        var answer = pollAnswer.OptionIds.FirstOrDefault();
        var selectedOption = PollOptions[answer];
        if (pollAnswer.User != null)
            await _bot.SendTextMessageAsync(pollAnswer.User.Id, $"You've chosen: {selectedOption.Text} in poll");
    }

    #endregion
}
