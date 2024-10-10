using DAL.Models;

namespace EcoAdviceAppApi.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
     

        public int? ParentPostId { get; set; }
       
    }
}
