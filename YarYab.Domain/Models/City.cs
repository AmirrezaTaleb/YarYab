namespace YarYab.Domain.Models
{
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
