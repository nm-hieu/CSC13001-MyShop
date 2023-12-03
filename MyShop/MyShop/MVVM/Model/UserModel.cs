using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        public int ID {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
