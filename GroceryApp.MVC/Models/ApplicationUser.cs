using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Models
{
    public class ApplicationUser : IApplicationUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
