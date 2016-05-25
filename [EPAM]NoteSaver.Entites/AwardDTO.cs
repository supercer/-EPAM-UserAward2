
namespace _EPAM_Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AwardDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }

        public AwardDTO(string title)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
        }

        public AwardDTO(string title, int? image_id)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.ImageId = image_id;
        }

        public AwardDTO(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public AwardDTO(Guid id, string title, int? image_id)
        {
            this.Id = id;
            this.Title = title;
            this.ImageId = image_id;
        }

        public AwardDTO()
        {

        }
    }
}

  