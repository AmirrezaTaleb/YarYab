using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YarYab.Domain.Models;

namespace YarYab.Service.DTO
{
    public class SendRequestDTO : BaseDto<SendRequestDTO, Request>
    {
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public string RequestMessage { get; set; }

    }
       public class UpdateRequestStatusDTO : BaseDto<UpdateRequestStatusDTO, Request>
    {
        [Required]
        public int RequestId { get; set; }
         [Required]
        public RequestStatus StatusId { get; set; }
 
    }

}
