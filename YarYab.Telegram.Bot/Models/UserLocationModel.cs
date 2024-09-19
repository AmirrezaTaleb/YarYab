using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Telegram.Bot.Models
{
    public record struct UserLocationModel
    {
        public UserLocationModel(int userId, float lat, float lang)
        {
            this.UserId = userId;
            this.Lat = lat;
            this.Lang = lang;
        }

        public int UserId { get; init; }
        public float Lat { get; init; }
        public float Lang { get; init; }
    }
}
