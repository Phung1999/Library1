using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCommanCore
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public  string RoleID { get; set; }
        public string GroupID { set; get; }
    }
}
