namespace YarYab.Domain.Models
{
    public class Request : BaseEntity
    {
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
        public string Status { get; set; }
        public string RequestMessage { get; set; }
  
    }
}
