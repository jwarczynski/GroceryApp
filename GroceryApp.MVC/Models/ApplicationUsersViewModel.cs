

using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Models
{
    public class ApplicationUsersViewModel
    {
        private readonly IDAO _blc;
        private readonly List<IApplicationUser> _users;
        private IApplicationUser? _user;
        public IApplicationUser? User { get { return _user; } }
        public ApplicationUsersViewModel(IDAO blc)
        {
            _blc = blc;
            _users = blc.GetApplicationUsers().ToList();
            _user = null;
        }
        public void RegisterUser(string name, string password)
        {
            var res = _blc.SaveApplicationUser(new ApplicationUser() { Name = name, Password = password });
            if (res != null)
            {
                _users.Append(res);
            }
        }
        public void ChangePassword(IApplicationUser user)
        {
            if (_user != null)
            {
                _blc.EditApplicationUser(user);
            }
        }
        public void DeleteUser()
        {
            if (_user == null) return;
            _blc.DeleteApplicationUser(_user);
            _users.Remove(_user);
            _user = null;
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

        public void LogOut()
        {
            _user = null;
        }

        public bool IsLoggedIn()
        {
            return _user != null;
        }
    }
}
