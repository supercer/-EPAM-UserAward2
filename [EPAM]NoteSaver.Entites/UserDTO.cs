
namespace _EPAM_Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBith { get; set; }
        public int? ImageId { get; set; }
    
        public int Age
        {
            get { return TempAge(); }
        }

        public UserDTO(string name, DateTime date_of_bith)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.DateOfBith = date_of_bith;
        }

        public UserDTO(string name, DateTime date_of_bith, int? image_id)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.DateOfBith = date_of_bith;
            this.ImageId = image_id;
        }

        public UserDTO(Guid id, string name, DateTime date_of_bith)
        {
            this.Id = id;
            this.Name = name;
            this.DateOfBith = date_of_bith;
        }

        public UserDTO(Guid id, string name, DateTime date_of_bith, int? image_id)
        {
            this.Id = id;
            this.Name = name;
            this.DateOfBith = date_of_bith;
            this.ImageId = image_id;
        }

        public UserDTO()
        {

        }

       private int TempAge()
        {
            DateTime now = DateTime.Today;
            int temp_age = now.Year - this.DateOfBith.Year;
            if ((temp_age >= 1) && ((now.Month < this.DateOfBith.Month)
                 || ((now.Month == this.DateOfBith.Month) && (now.Day < this.DateOfBith.Day))))
            {
                --temp_age;
            }

            return temp_age;
        }
    }
}
