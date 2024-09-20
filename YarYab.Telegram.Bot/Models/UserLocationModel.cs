using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Telegram.Bot.Models
{
    public record struct UserLocationModel
    {
        public UserLocationModel(  double lat, double lang)
        {
             this.Lat = lat;
            this.Lang = lang;
        }

        public int UserId { get; init; }
        public double Lat { get; init; }
        public double Lang { get; init; }
    }
}
