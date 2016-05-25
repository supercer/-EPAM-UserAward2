
namespace _EPAM_DALCollection
{
    using _EPAM_Intefases.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    public class DALUser : IUserDAL
    {
        public static List<UserDTO> Users = new List<UserDTO>();
        public bool Create(UserDTO note)
        {
            try
            {
                Users.Add(note);
                return true;
            }
            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет записи юзера", e);
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var temp = Users.FirstOrDefault(x => x.Id == id);

                Users.Remove(temp);
                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет данной записи", e);
            }
        }

        public UserDTO Get(Guid id)
        {
            try
            {
                var temp = Users.FirstOrDefault(x => x.Id == id);
                return temp;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<UserDTO> GetAll()
        {
            foreach (var item in Users)
            {
                yield return item;
            }

        }

        public bool Update(UserDTO user)
        {
            try
            {
                int c = 0;
                foreach (var item in Users)
                {
                    if (item.Id == user.Id)
                    {
                        Users[c] = user;
                        return true;
                    }

                    c++;
                }

                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
    }
}
