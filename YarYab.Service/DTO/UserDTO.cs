using System.ComponentModel.DataAnnotations;
using YarYab.Domain.Models;
using YarYab.Service.DTO;

namespace YarYab.API.DTO
{
    public class AddSimpleUserDTO : BaseDto<AddSimpleUserDTO, User>
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public short Age { get; set; }
        [Required]
        public UserGender Gender { get; set; }
        [Required]
        public string ChanelId { get; set; }
    }
    public class EditUserDTO : BaseDto<EditUserDTO, User>
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public short Age { get; set; }
        [Required]
        public UserGender Gender { get; set; }
        [Required]
        public string ChanelId { get; set; }
        public int? CityId { get; set; }
        public string? ProfilePhoto { get; set; }
    }

}
