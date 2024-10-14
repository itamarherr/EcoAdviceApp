using DAL.Models;
using EcoAdviceAppApi.DTOs;

namespace EcoAdviceAppApi.Mappings
{
    public static class PostDtoExtensions
    {
        public static PostDto ToDto(this Post p)
        {
            return new PostDto()
            {
                Id = p.Id,
                Content = p.Content,
                PostDate = p.PostDate,
                UserId = p.UserId,
                UserName = p.User?.Name ?? "Unknown User",
                ParentPostId = p.ParentPostId,

            };
        }
        public static Post ToEntity(this PostDto dto)
        {
            return new Post()
            {
                Id = dto.Id,
                Content = dto.Content,
                PostDate = dto.PostDate,
                UserId = dto.UserId,
                ParentPostId = dto.ParentPostId,

            };
        }
    }
}
