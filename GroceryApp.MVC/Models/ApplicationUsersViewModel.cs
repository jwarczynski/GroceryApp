

using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Models
{
    public class ApplicationUsersViewModel
    {
        private readonly IDAO _blc;
        private readonly List<IApplicationUser> _users;
        private IApplicationUser? _user;
        public ApplicationUsersViewModel(IDAO blc)
        {
            _blc = blc;
            _users = blc.GetApplicationUsers().ToList();
            _user = null;
        }
        public void RegisterUser(IApplicationUser user)
        {
            var res = _blc.SaveApplicationUser(user);
            if (res != null)
            {
                _users.Append(res);
            }
        }
        public void DeleteUser(string name)
        {
            var users = _users.Where(u => u.Name == name);
            foreach (var  user in users)
            {
                _blc.DeleteApplicationUser(user);
                _users.Remove(user);
            }
        }
        public bool LogIn(string name, string password)
        {
            var user = _users.Where(u => u.Name == name && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                _user = user;
                return true;
            }
            return false;
        }

        public bool IsLoggedIn()
        {
            return _user != null;
        }
    }
}
