using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_Entites;

namespace _EPAM_Intefases.DAL
{
    public interface IRoleDAL
    {
        IEnumerable<RoleDTO> GetAll();
        RoleDTO Get(int id);
        bool Create(RoleDTO note);
        bool Delete(int id);
        bool Update(RoleDTO note);
        int GetIdAnRoleName(string role_name);
    }
}
