using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class UserInfo
    {

        private static UserInfo _user;

        public static UserInfo User
        {
            get
            {
                if (_user == null)
                    throw new Exception("User undefined!");
                return _user;
            }
        }

        public string Name { get; set; }

        private UserInfo()
        {

        }

        public static void Configure(string name)
        {
            UserInfo User = new UserInfo();
            User.Name = name;
            _user = User;
        }
    }
}
