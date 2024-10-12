using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

namespace DAL.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string UserType { get; set; }

        //public ICollection<Post> Posts { get; set; }
    }
}
