using Microsoft.AspNetCore.Http;
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
    public class SetUserLocationDTO : BaseDto<SetUserLocationDTO, User>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

    }
    public class GetAllUserSelectDTO
    {
        public UserGender? Gender { get; set; }
        public int? City_Id { get; set; }
        public short? Age { get; set; }
        public GetNearUserSelectDTO? Location { get; set; }
    }
    public class GetCitiesSelectDTO
    {
        public int? Parent_Id { get; set; }
    }
    public class GetNearUserSelectDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class UserScoreSelectDTO
    {
        public int? Update_Score { get; set; }
        public int User_id { get; set; }


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
    }

}
