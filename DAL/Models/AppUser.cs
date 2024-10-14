using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

namespace DAL.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
    
    }
}
