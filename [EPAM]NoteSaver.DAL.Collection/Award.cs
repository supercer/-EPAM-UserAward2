
namespace _EPAM_DALCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using _EPAM_Intefases.DAL;
    public class DALAward:IAwardDAL
    {
        public static List<AwardDTO> Awards = new List<AwardDTO>();
        public bool Create(AwardDTO note)
        {
            try
            {
                Awards.Add(note);
                return true;
            }
            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет записи награды", e);
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var temp = Awards.FirstOrDefault(x => x.Id == id);

                Awards.Remove(temp);
                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет данной записи", e);
            }
        }

        public AwardDTO Get(Guid id)
        {
            try
            {
                var temp = Awards.FirstOrDefault(x => x.Id == id);
                return temp;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<AwardDTO> GetAll()
        {
            foreach (var item in Awards)
            {
                yield return item;
            }

        }

        public bool Update(AwardDTO award)
        {
            try
            {
                int c = 0;
                foreach (var item in Awards)
                {
                    if (item.Id == award.Id)
                    {
                        Awards[c] = award;
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

