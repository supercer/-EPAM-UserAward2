using _EPAM_Intefases.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Auth
    {
        public static bool CanLogin(string login, string password)
        {
            IWebUserBLL web_user_logic = new _EPAM_BLL.WebUserLogic();

            return web_user_logic.IsWebUser(login, password);        
        }
    }
}