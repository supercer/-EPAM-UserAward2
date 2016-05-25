using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_Entites;

namespace _EPAM_Intefases.BLL
{
   public interface IWebUserBLL
    {
        IEnumerable<WebUserDTO> GetAll();
        WebUserDTO Get(int id);
        bool Create(WebUserDTO note);
        bool Delete(int id);
        bool Update(WebUserDTO note);
        bool IsWebUser(string name, string password);
        int GetIdAnWebUserName(string web_user_name);
    }
}
