using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Domain.Models
{
    public class Contact : BaseEntity
    {
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? FriendId { get; set; }
        public virtual User? Friend { get; set; }

    }

}
