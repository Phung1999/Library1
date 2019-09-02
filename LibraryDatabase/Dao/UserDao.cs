using System.Linq;
using LibraryDatabase.Database;
using System.Collections.Generic;
using LibraryCommanCore;

namespace LibraryDatabase.Dao
{
    public  class UserDao
    {
        public readonly LibraryDBEntities db = null;

        public UserDao()
        {
            db=new LibraryDBEntities();
        }
        public User GetById(string userName)
        {
            return db.Users.FirstOrDefault(x => x.UserName == userName);
        }
        public string GetRole(string userName)
        {
            //string resultRole=null;
            //var result = db.Users.FirstOrDefault(x => x.username == userName);
            //if (result. == 1)
               string resultRole = "View";
            return resultRole;

        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.FirstOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {

                    if (result.IDUserGroup==CommonConstants.ADMIN_GROUP||result.IDUserGroup==CommonConstants.MOD_GROUP)
                    {
                        if (result.IsBlocked == false)
                        {
                            //return -1;
                            if (result.PassWord.Trim() == passWord)
                                return 1;
                            else
                                return -2;
                        }
                        else
                        {
                            if (result.PassWord.Trim() == passWord)
                                return 1;
                            else
                                return -2;
                        }                
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.IsBlocked == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        //public List<string> GetListCredential(string userName)
        //{
        //    var user = db.Users.Single(x => x.UserName == userName);
        //    var data = (from a in db.Credentials
        //                join b in db.UserGroups on a.UserGroupID equals b.UserGroupID
        //                join c in db.UserRoles on a.IDRole equals c.UserRoleID
        //        where b.UserGroupID == user.IDUserGroup
        //                select new
        //                {
        //                    RoleID = a.IDRole,
        //                    UserGroupID = a.UserGroupID
        //                }).AsEnumerable().Select(x => new Credential()
        //                {
        //                    IDRole = x.RoleID,
        //                    UserGroupID = x.UserGroupID
        //                });
        //    return data.Select(x => x.IDRole).ToList();

        //}
    }

}
