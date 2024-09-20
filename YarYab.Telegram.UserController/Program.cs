using var client = new WTelegram.Client(Config);
var myself = await client.LoginUserIfNeeded();
Console.WriteLine($"We are logged-in as {myself} (id {myself.id})");


var chats = await client.Messages_GetAllChats();
Console.WriteLine("This user has joined the following:");
foreach (var (id, chat) in chats.chats)
    if (chat.IsActive)
        Console.WriteLine($"{id,10}: {chat}");
Console.Write("Type a chat ID to send a message: ");
long chatId = long.Parse(Console.ReadLine());
var target = chats.chats[chatId];
Console.WriteLine($"Sending a message in chat {chatId}: {target.Title}");
await client.SendMessageAsync(target, "Hello, World");


static string Config(string what)
{
    switch (what)
    {
        case "api_id": return "YOUR_API_ID";
        case "api_hash": return "YOUR_API_HASH";
        case "phone_number": return "+12025550156";
        case "verification_code": Console.Write("Code: "); return Console.ReadLine();
        case "first_name": return "John";      // if sign-up is required
        case "last_name": return "Doe";        // if sign-up is required
        case "password": return "secret!";     // if user has enabled 2FA
        default: return null;                  // let WTelegramClient decide the default config
    }
}
