
namespace _EPAM_Intefases.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;

    public interface IAwardBLL
    {
        IEnumerable<AwardDTO> GetAll();
        AwardDTO Get(Guid id);
        bool Create(AwardDTO note);
        bool Delete(Guid id);
        bool Update(AwardDTO note);
        ImageDTO GetImage(int id);
        int CreateImage(ImageDTO note);
        bool SetImageToUser(Guid award_id, int image_id);
    }
}

