using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YarYab.Domain.Models;

namespace YarYab.Service.DTO
{
    public class AddContactDTO : BaseDto<AddContactDTO, Contact>
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int FriendId { get; set; }
 
    }
    public class GetContactDTO : BaseDto<GetContactDTO, Contact>
    {
         public int ContactId { get; set; }
         public User Friend { get; set; }

    }
}
