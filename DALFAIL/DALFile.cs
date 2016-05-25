using _EPAM_User.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_User.Interfases.DAL;
using System.Configuration;
using Logger;

namespace _EPAM_User.DAL.File
{
    public class DALFile : IUserDAL
    {
        public static List<UserDTO> Users = new List<UserDTO>();
        public FileInfo file { get; set; }
        public DALFile()
        {
            try
            {
                this.file = new FileInfo(ConfigurationManager.AppSettings["DataBaseUser"]);
                if (!file.Exists)
                {
                    file.Create();
                }

                this.Load();
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        ~DALFile()
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



//private FileInfo file;
////public DalFail(FileInfo file)
////{
////    this.file = file;
////}

//public IEnumerable<User> GetAll()
//{
//    List<User> users = new List<User> ();
//    using (StreamReader read = new StreamReader(file.FullName))
//    {
//        string line = null;
//        User user = new User();
//     while(true)
//        {
//             line = read.ReadLine();
//            if(line == "User:")
//            {                        
//                user.Id = Guid.Parse(read.ReadLine());
//                user.Name = read.ReadLine();
//                user.DateOfBith = DateTime.Parse(read.ReadLine());
//                user.Age = int.Parse(read.ReadLine());
//                users.Add(user);
//                user = new User();
//            }

//            else
//            {
//                break;
//            }
//        }
//        return users;
//    }
//}

//public bool Create(User )
//{
//    if (file != null)
//    {
//        this.file = file;
//        this.file.Create();
//        return true;
//    }

//    else
//    {
//        throw new NullReferenceException();
//    }
//}

//public bool Delete(FileInfo file)
//{
//    if (file != null)
//    {
//        file.Delete();
//        return true;              
//    }

//    else
//    {
//        return false;
//    }

//}

//public FileInfo Get(FileInfo file)
//{
//    if (file.Exists)
//    {
//        this.file = file;
//        return this.file;
//    }

//    else
//    {
//        file = null;
//        return file;
//    }
//}

//