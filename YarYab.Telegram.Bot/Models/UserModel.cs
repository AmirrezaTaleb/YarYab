namespace YarYab.Telegram.Bot.Models
{
    public record struct UserModel
    {
        public string UserId { get; set; }
        public string  Name { get; set; }
        public int Age { get; set; }
        public UserLocationModel Location { get; set; }
        public DateTime LastSeen{ get; set; }
        public DateTime LastActivity{ get; set; }
        public string CityTitle{ get; set; }
 

    }
}
