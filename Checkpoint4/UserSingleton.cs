using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    class UserSingleton
    {
        public Person user { get; set; }
        private static UserSingleton _instance = null;
        public static UserSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSingleton();
                }
                return _instance;
            }
        }
    }
}
