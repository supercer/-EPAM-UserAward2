
namespace _EPAM_Intefases.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;

    public interface IAwardDAL
    {
        IEnumerable<AwardDTO> GetAll();
        AwardDTO Get(Guid id);
        bool Create(AwardDTO note);
        bool Delete(Guid id);
        bool Update(AwardDTO note);
    }
}
