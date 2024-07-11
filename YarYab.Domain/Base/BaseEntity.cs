using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Domain
{
    public interface IEntity
    {
    }


    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }

}
