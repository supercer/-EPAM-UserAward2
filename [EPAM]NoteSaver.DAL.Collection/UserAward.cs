
namespace _EPAM_DALCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using _EPAM_Intefases.DAL;

    public class DALUserAward :IUserAwardDAL
    {
        public static List<UserAwardDTO> UsersAwards = new List<UserAwardDTO>();

        public bool Create(UserAwardDTO note)
        {
            try
            {
                UsersAwards.Add(note);
                return true;
            }
            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет записи юзера и награды", e);
            }
        }

        public bool Delete(UserAwardDTO user_award)
        {
            try
            {
                foreach (var item in UsersAwards)
                {
                    if (item == user_award)
                    {
                        UsersAwards.Remove(item);
                        break;
                    }

                }

                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет данной записи", e);
            }
        }

        //public UserAwardDTO Get(UserAwardDTO user_award)
        //{
        //    try
        //    {
        //        UserAwardDTO temp = new UserAwardDTO();
        //        foreach (var item in UsersAwards)
        //        {
        //            if (item == user_award)
        //            {
        //                temp = item;
        //                break;
        //            }
        //        }
        //        return temp;
        //    }

        //    catch (Exception e)
        //    {
        //        Logger.Logger.WriteLog(e);
        //        throw e;
        //    }
        //}

        public IEnumerable<UserAwardDTO> GetAll()
        {
            foreach (var item in UsersAwards)
            {
                yield return item;
            }

        }
    }
}


