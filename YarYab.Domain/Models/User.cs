using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public string TelegramId { get; set; }
        public int? CityId { get; set; }
        public virtual City? City { get; set; }
         public string? ProfilePhoto { get; set; }
        public ICollection<Request>? RequestsSend { get; set; }
        public ICollection<Request>? RequestsRecive { get; set; }

    }
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int? ParentId { get; set; }
        public City Parent { get; set; }
        public ICollection<City> Children { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
