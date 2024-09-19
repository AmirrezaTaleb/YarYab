using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace YarYab.Telegram.Bot.Models
{
    public record struct UserProfileMessageModel
    {
        public string Banner { get; set; }
        public FileStream Photo { get; set; }
        public InlineKeyboardMarkup InlineKeyboardMarkup { get; set; }
    }
}
