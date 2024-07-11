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
        public short Age { get; set; }
        public UserGender Gender { get; set; }
         public string ChanelId { get; set; }
        public int? CityId { get; set; }
        public virtual City? City { get; set; }
         public string? ProfilePhoto { get; set; }
        public ICollection<Request>? RequestsSend { get; set; }
        public ICollection<Request>? RequestsRecive { get; set; }

    }
    public enum UserGender
    {
        Man,
        Woman
    }
}
