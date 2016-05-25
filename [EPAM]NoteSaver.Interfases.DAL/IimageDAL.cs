using _EPAM_Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_Intefases.DAL
{
    public interface IimageDAL
    {
        ImageDTO Get(int id);
        int Create(ImageDTO note);
    }
}
