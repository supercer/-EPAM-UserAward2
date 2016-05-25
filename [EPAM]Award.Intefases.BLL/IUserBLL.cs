
namespace _EPAM_Intefases.BLL
{
    using _EPAM_Entites;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserBLL
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(Guid id);
        bool Create(UserDTO note);
        bool Delete(Guid id);
        bool Update(UserDTO note);
        bool GetNames();
        IEnumerable<DateTime> GetDateOfBith();
        IEnumerable<int> GetAge();
        double MiddleAge();
        int SumAge();
        ImageDTO GetImage(int id);
        int CreateImage(ImageDTO note);
        bool SetImageToUser(Guid user_id, int image_id);
    }
}
