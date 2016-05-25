
namespace _EPAM_Intefases.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;

    public interface IUserAwardBLL
    {
        IEnumerable<UserAwardDTO> GetAll();
        bool Create(UserAwardDTO note);
        bool Delete(UserAwardDTO note);
        Dictionary<UserDTO, List<AwardDTO>> AwardsByUser();
        Dictionary<AwardDTO, List<UserDTO>> UsersByAward();
        ImageDTO GetImage(int id);
        int CreateImage(ImageDTO note);
    }
}
