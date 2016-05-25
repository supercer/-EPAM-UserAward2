

namespace _EPAM_DALFile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using System.IO;
    using System.Configuration;
    using _EPAM_Intefases.DAL;

    public class DALFileAward: IAwardDAL
    {
        public static List<AwardDTO> Awards = new List<AwardDTO>();
        public FileInfo file { get; set; }
        public DALFileAward()
        {
            try
            {
                this.file = new FileInfo(ConfigurationManager.AppSettings["DataBaseAward"]);
                if (!this.file.Exists)
                {
                    file.Create().Close();

                }
                if (Awards.Count == 0)
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

        ~DALFileAward()
        {
            Save();
        }

        public IEnumerable<AwardDTO> GetAll()
        {
            foreach (var item in Awards)
            {
                yield return item;
            }

        }

        public IEnumerable<AwardDTO> Load()
        {
            try
            {
                using (StreamReader read = new StreamReader(file.FullName))
                {
                    string line = null;
                    AwardDTO award = new AwardDTO();
                    while (true)
                    {
                        line = read.ReadLine();
                        if (line == "Award:")
                        {
                            award.Id = Guid.Parse(read.ReadLine());
                            award.Title = read.ReadLine();
                            Awards.Add(award);
                            award = new AwardDTO();
                        }

                        else
                        {
                            break;
                        }
                    }
                }
                return Awards;
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Create(AwardDTO note)
        {
            try
            {
                Awards.Add(note);
                return true;
            }
            catch (Exception e)
            {
                throw new NullReferenceException("нет записи награждения", e);
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

        bool Save()
        {
            try
            {
                this.file.Delete();
                this.file.Create();
                using (StreamWriter stream = new StreamWriter(file.FullName))
                {
                    foreach (var item in Awards)
                    {
                        stream.WriteLine("Award:");
                        stream.WriteLine(item.Id.ToString("D"));
                        stream.WriteLine(item.Title);
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
    }
}

