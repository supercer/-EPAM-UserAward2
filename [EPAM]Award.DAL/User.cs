
namespace _EPAM_DALFile
{
    using _EPAM_Entites;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Intefases.DAL;
    using System.Configuration;

    public class DALFileUser : IUserDAL
    {
        public static List<UserDTO> Users = new List<UserDTO>();
        public FileInfo file { get; set; }
        public DALFileUser()
        {
            try
            {
                this.file = new FileInfo(ConfigurationManager.AppSettings["DataBaseUser"]);
                if (!file.Exists)
                {
                    file.Create();
                }
                if (Users.Count == 0)
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

        ~DALFileUser()
        {
            this.Save();
        }

        public IEnumerable<UserDTO> GetAll()
        {
                foreach (var item in Users)
                {
                    yield return item;
                }
            }      
       
    private IEnumerable<UserDTO> Load()
    {
        using (StreamReader read = new StreamReader(file.FullName))
        {
            string line = null;
            UserDTO user = new UserDTO();
            while (true)
            {
                line = read.ReadLine();
                if (line == "User:")
                {
                    user.Id = Guid.Parse(read.ReadLine());
                    user.Name = read.ReadLine();
                    user.DateOfBith = DateTime.Parse(read.ReadLine());
                    Users.Add(user);
                    user = new UserDTO();
                }

                else
                {
                    break;
                }
            }
            return Users;
        }
    }
      

        public bool Create(UserDTO note)
        {
            try
            {
                Users.Add(note);
                return true;
            }
            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new NullReferenceException("нет записи юзера",e);
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

            catch(Exception e)
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

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
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
                    }

                    c++;
                }

                return true;
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        private bool Save()
        {
            try
            {
                this.file.Delete();
                this.file.Create();
                using (StreamWriter stream = new StreamWriter(file.FullName))
                {
                    foreach (var item in Users)
                    {
                        stream.WriteLine("User:");
                        stream.WriteLine(item.Id.ToString("D"));
                        stream.WriteLine(item.Name);
                        stream.WriteLine(item.DateOfBith);
                    }
                }
                return true;
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
    }
    }
