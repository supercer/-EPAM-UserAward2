using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_Entites
{
    public class WebUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PasswordHashCode { get; set; }

        public WebUserDTO(string name, int password_hash_code)
        {
            this.Name = name;
            this.PasswordHashCode = password_hash_code;
        }

        public WebUserDTO(int id, string name, int password_hash_code)
        {
            this.Id = id;
            this.Name = name;
            this.PasswordHashCode = password_hash_code;
        }
    }
}
