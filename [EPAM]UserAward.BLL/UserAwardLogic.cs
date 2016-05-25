
namespace _EPAM_UserAward.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_UserAward.Entites;
    using _EPAM_UserAward.DAL;
    using _EPAM_UserAward.Interfases.BLL;

    public class UserAwardLogic : IUserAwardBLL
    {
        private _EPAM_UserAward.Interfases.DAL.IUserAwardDAL dal_user_award;
        private _EPAM_User.Interfases.DAL.IUserDAL dal_users;
        private _EPAM_Award.Intefases.DAL.IAwardDAL dal_awards;
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
                            dal_users = new _EPAM_User.DAL.File.DALFile();
                            dal_awards = new _EPAM_Award.DAL.DALFile();
                            dal_user_award = new _EPAM_UserAward.DAL.DALFile();
                        }

                        break;
                    case "Collection":
                        {
                            dal_users = new _EPAM_User.DAL.Collection.DAL();
                            dal_awards = new _EPAM_Award.DAL.DALCollection();
                            dal_user_award = new _EPAM_UserAward.DAL.DALCollection();
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

        public UserAwardDTO Get(UserAwardDTO note)
        {
            try
            {
                return dal_user_award.Get(note);
            }

            catch (Exception e)
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
    }
}