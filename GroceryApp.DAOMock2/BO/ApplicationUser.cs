using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock2.BO
{
    internal class ApplicationUser : IApplicationUser
    {
        int? IApplicationUser.Id { get; set; }
        string IApplicationUser.Name { get; set; }
        string IApplicationUser.Password { get; set; }
    }
}
