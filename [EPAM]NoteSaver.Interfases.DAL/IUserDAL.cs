
namespace _EPAM_Intefases.DAL
{
    using _EPAM_Entites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserDAL
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(Guid id);
        bool Create(UserDTO note);
        bool Delete(Guid id);
        bool Update(UserDTO user);
    }
}
