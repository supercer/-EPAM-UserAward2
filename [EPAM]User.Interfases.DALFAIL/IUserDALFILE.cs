using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_User.Entites;
using System.IO;

namespace _EPAM_User.Interfases.DALFILE
{
    public interface IUserDALFILE 
    {
        FileInfo GetFile();
        bool CreateFile(string way);
        bool DeleteFile();
        StreamWriter UpdateFile(StreamWriter stream);
    }
}
