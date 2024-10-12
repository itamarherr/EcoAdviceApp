using DAL.Models;
using EcoAdviceAppApi.DTOs;

namespace EcoAdviceAppApi.Mappings;

public static class RgisterDtoExtensions
{
    public static AppUser ToUser(this RegisterDto dto)
    {
        return new AppUser()
        {
            Email = dto.Email,
            UserName = dto.Email,
            Name = dto.Name
        };



     }
}
