
namespace _EPAM_Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserAwardDTO
    {
        public Guid UserId { get; set; }
        public Guid AwardId { get; set; }
        public UserAwardDTO()
        {

        }

        public UserAwardDTO(Guid user_id, Guid award_id)
        {
            this.UserId = user_id;
            this.AwardId = award_id;
        }
    }
}
