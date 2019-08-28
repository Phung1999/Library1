using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;

namespace LibraryCommanCore
{
    public static class CreateFolder
    {
        public static string FolderParent(string folder)
        {
            string folderPath = HttpContext.Current.Server.MapPath("~/" + folder);
            try
            {
                //HttpRuntime.AppDomainAppPath
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                return folder;
            }
            catch (Exception e)
            {

                return "" + e;
            }
        }
        public static string FolderChildren(string folder)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/" + folder);
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                return folder;
            }
            catch (Exception e)
            {

                return "" + e;
            }

        }

    }
}
