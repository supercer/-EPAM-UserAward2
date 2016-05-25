using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_Entites;

namespace _EPAM_Intefases.DAL
{
    public interface IWebUserRoleDAL
    {
        IEnumerable<WebUserRoleDTO> GetAll();
        WebUserRoleDTO Get(int id);
        bool Create(WebUserRoleDTO note);
        bool Delete(WebUserRoleDTO id);
        bool Update(WebUserRoleDTO note);
        bool IsWebUserInRole(string name, string role);
        string[] GetRolesForUser(string web_user_name);
    }
}
