
namespace _EPAM_BLL
{
    using _EPAM_Intefases.BLL;
    using _EPAM_Intefases.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using System.Configuration;
    using System.IO;
    using _EPAM_DALDATABASE;
    public class UserLogic : IUserBLL
    {
        private _EPAM_Intefases.DAL.IUserDAL dal;
        private _EPAM_Intefases.DAL.IimageDAL dal_image;

        public UserLogic()
        {
            string mode;
            try
            {
                mode = ConfigurationManager.AppSettings["DataMode"];
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw new Exception("Some problem with configuration file", e);
            }


            try
            {
                switch (mode)
                {
                    case "Files":
                        {
                            dal = new _EPAM_DALFile.DALFileUser();
                        }

                        break;
                    case "Collection":
                        {
                            dal = new _EPAM_DALCollection.DALUser();
                        }

                        break;
                    case "DATABASE":
                        {
                            dal = new _EPAM_DALDATABASE.User();
                            dal_image = new _EPAM_DALDATABASE.Image();
                        }

                        break;
                }
            }
            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
            }

        public bool Create(UserDTO note)
        {
            try
            {
                return dal.Create(note);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                return dal.Delete(id);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public UserDTO Get(Guid id)
        {
            try
            {
                return dal.Get(id);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public ImageDTO GetImage(int id)
        {
            try
            {
                return dal_image.Get(id);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public int CreateImage(ImageDTO note)
        {
            try
            {
                return dal_image.Create(note);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return dal.GetAll().ToArray();
        }

        public bool Update(UserDTO user)
        {
            try
            {
                dal.Update(user);
                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool GetNames()
        {
            try
            {
                foreach (var item in dal.GetAll())
                {
                    Console.WriteLine(item.Name);
                }
                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<DateTime> GetDateOfBith()
        {
            List<DateTime> dateOfBiths = new List<DateTime>();
           
            try
            { 
            foreach (var item in dal.GetAll())
            {
                dateOfBiths.Add(item.DateOfBith);
            }
            return dateOfBiths;
        }

        catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<int> GetAge()
        {
            List<int> ages = new List<int>();
            try
            {
                foreach (var item in dal.GetAll())
                {
                    ages.Add(item.Age);
                }
                return ages;
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public double MiddleAge()
        {
            try
            {
                int sum = 0;
                int count = 0;
                foreach (var item in dal.GetAll())
                {
                    sum += item.Age;
                    count++;
                }

                return sum / count ;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public int SumAge()
        {
            try
            {
                int sum = 0;
                foreach (var item in dal.GetAll())
                {
                    sum += item.Age;
                }

                return sum;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool SetImageToUser(Guid user_id, int image_id)
        {
            UserDTO user;
            try
            {
                user = dal.Get(user_id);
                user.ImageId = image_id;
                return dal.Update(user);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
    }
}
