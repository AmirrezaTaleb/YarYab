using System.ComponentModel.DataAnnotations;

namespace YarYab.API.DTO
{
    public class AddSimpleUserDTO/* : BaseDto<UserDto, User>, IValidatableobject*/
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

    }
}
