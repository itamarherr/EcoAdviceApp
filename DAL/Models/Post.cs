namespace DAL.Models
{
    public class Post
    {
        public int Id {  get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public int UserId {  get; set; }
        public User User { get; set; }

        public int? ParentPostId {  get; set; }
        public Post ParentPost { get; set; }

        public ICollection<Post> replies { get; set;}
    }
}
