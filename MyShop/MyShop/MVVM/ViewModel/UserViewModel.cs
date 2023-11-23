using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    class UserViewModel : ObservableObject
    {
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _fullName;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Fullname
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }

        //public UserViewModel()
        //{
        //    userRepository = new UserRepository();
        //    loadAllUser();
        //}
        //private void loadAllUser()
        //{
        //    var userList = userRepository.loadAllUser();
        //    if (userList != null)
        //    {
        //        Username = "Username";
        //        Fullname = "Fullname";
        //    }
        //}

    }
}
