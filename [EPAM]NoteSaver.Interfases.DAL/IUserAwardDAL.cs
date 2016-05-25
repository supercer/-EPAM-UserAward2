
namespace _EPAM_Intefases.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;

    public interface IUserAwardDAL
    {
        IEnumerable<UserAwardDTO> GetAll();
        bool Create(UserAwardDTO note);
        bool Delete(UserAwardDTO note);
    }
}
