
namespace _EPAM_BLL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using _EPAM_DALCollection;
    using _EPAM_Intefases.BLL;
    using _EPAM_Intefases.DAL;
    public class UserAwardLogic : IUserAwardBLL
    {
        private _EPAM_Intefases.DAL.IUserAwardDAL dal_user_award;
        private _EPAM_Intefases.DAL.IUserDAL dal_users;
        private _EPAM_Intefases.DAL.IAwardDAL dal_awards;
        private _EPAM_Intefases.DAL.IimageDAL dal_image;
        public UserAwardLogic()
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
                            dal_users = new _EPAM_DALFile.DALFileUser();
                            dal_awards = new _EPAM_DALFile.DALFileAward();
                            dal_user_award = new _EPAM_DALFile.DALFileUserAward();
                        }

                        break;
                    case "Collection":
                        {
                            dal_users = new _EPAM_DALCollection.DALUser();
                            dal_awards = new _EPAM_DALCollection.DALAward();
                            dal_user_award = new  _EPAM_DALCollection.DALUserAward();
                        }

                        break;
                    case "DATABASE":
                        {
                            dal_users = new _EPAM_DALDATABASE.User();
                            dal_awards = new _EPAM_DALDATABASE.Award();
                            dal_user_award = new _EPAM_DALDATABASE.UserAward();
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

        public IEnumerable<UserAwardDTO> GetAll()
        {
            try
            {
                return dal_user_award.GetAll().ToArray();
            }

            catch ( Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public Dictionary<UserDTO, List<AwardDTO>> AwardsByUser()
        {
            try
            {
                var result = dal_user_award.GetAll().GroupBy(x => x.UserId);
                Dictionary<UserDTO, List<AwardDTO>> r = new Dictionary<UserDTO, List<AwardDTO>>();
                List<AwardDTO> awards = new List<AwardDTO>();
                foreach (var item in result)
                {
                    foreach (var item2 in item)
                    {
                        awards.Add(this.dal_awards.Get(item2.AwardId));
                    }

                    r.Add(this.dal_users.Get(item.Key), awards);
                    awards = new List<AwardDTO>();
                }

                return r;
            }

            catch(Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public Dictionary<AwardDTO, List<UserDTO>> UsersByAward()
        {
            try
            {
                var result = dal_user_award.GetAll().GroupBy(x => x.AwardId);
                Dictionary<AwardDTO, List<UserDTO>> r = new Dictionary<AwardDTO, List<UserDTO>>();
                List<UserDTO> users = new List<UserDTO>();
                foreach (var item in result)
                {
                    foreach (var item2 in item)
                    {
                        users.Add(this.dal_users.Get(item2.UserId));
                    }

                    r.Add(this.dal_awards.Get(item.Key), users);
                    users = new List<UserDTO>();
                }

                return r;
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
                return dal_user_award.Create(note);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Delete(UserAwardDTO note)
        {
            try
            {
                return dal_user_award.Delete(note);
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

    }
}