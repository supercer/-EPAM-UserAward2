using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_Entites
{
    public class WebUserRoleDTO
    {
        public int Id { get; set; }
        public int WebUserId { get; set; }
        public int RoleId { get; set; }

        public WebUserRoleDTO(int web_user_id, int role_id)
        {
            this.WebUserId = web_user_id;
            this.RoleId = role_id;
        }

        public WebUserRoleDTO(int id, int web_user_id, int role_id)
        {
            this.Id = id;
            this.WebUserId = web_user_id;
            this.RoleId = role_id;
        }
    }
}
