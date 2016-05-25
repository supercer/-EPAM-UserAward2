
namespace _EPAM_DALFile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using _EPAM_Intefases.DAL;
    using System.IO;
    using System.Configuration;

    public class DALFileUserAward: IUserAwardDAL
    {
        public static List<UserAwardDTO> UsersAwards = new List<UserAwardDTO>();
        public FileInfo file { get; set; }
        public DALFileUserAward()
        {
            try
            {
                this.file = new FileInfo(ConfigurationManager.AppSettings["DataBaseUserAward"]);
                if (!this.file.Exists)
                {
                    file.Create().Close();

                }
                if (UsersAwards.Count == 0)
                {
                    this.Load();
                }
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        ~DALFileUserAward()
        {
            Save();
        }

        public IEnumerable<UserAwardDTO> Load()
        {
            using (StreamReader read = new StreamReader(file.FullName))
            {
                string line = null;
                UserAwardDTO user_award = new UserAwardDTO();
                while (true)
                {
                    line = read.ReadLine();
                    if (line == "UserAward:")
                    {
                        user_award.UserId = Guid.Parse(read.ReadLine());
                        user_award.AwardId = Guid.Parse(read.ReadLine());
                        UsersAwards.Add(user_award);
                        user_award = new UserAwardDTO();
                    }

                    else
                    {
                        break;
                    }
                }
                return UsersAwards;
            }
        }

        bool Save()
        {
            try
            {
                this.file.Delete();
                this.file.Create();
                using (StreamWriter stream = new StreamWriter(file.FullName))
                {
                    foreach (var item in UsersAwards)
                    {
                        stream.WriteLine("UserAward:");
                        stream.WriteLine(item.UserId.ToString("D"));
                        stream.WriteLine(item.AwardId.ToString("D"));
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
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
                throw new NullReferenceException("нет записи", e);
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

        public UserAwardDTO Get(UserAwardDTO user_award)
        {
            try
            {
                UserAwardDTO temp = new UserAwardDTO();
                foreach (var item in UsersAwards)
                {
                    if (item == user_award)
                    {
                        temp = item;
                        break;
                    }
                }
                return temp;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<UserAwardDTO> GetAll()
        {
            foreach (var item in UsersAwards)
            {
                yield return item;
            }

        }
    }
}
  
