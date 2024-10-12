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
                UserName = p.User?.Name,
                ParentPostId = p.ParentPostId,

            };
        }
    }
}
