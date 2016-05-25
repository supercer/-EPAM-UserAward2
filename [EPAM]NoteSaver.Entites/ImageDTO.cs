using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_Entites
{
   public class ImageDTO
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public int Id { get; set; }

        public ImageDTO()
        {

        }

        public ImageDTO(int id, string content_type, byte[] content)
        {
            this.Id = id;
            this.ContentType = content_type;
            this.Content = content;
        }
       
    }
}
