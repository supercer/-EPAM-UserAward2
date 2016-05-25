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

namespace _EPAM_BLL
{
    public class WebUserRoleLogic: IWebUserRoleBLL
    {
        private _EPAM_Intefases.DAL.IWebUserRoleDAL dal_web_user_role;
        private _EPAM_Intefases.DAL.IRoleDAL dal_role;
        private _EPAM_Intefases.DAL.IWebUserDAL dal_web_user;
       
        public WebUserRoleLogic()
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
                    case "DATABASE":
                        {
                            dal_role = new _EPAM_DALDATABASE.Role();
                            dal_web_user = new _EPAM_DALDATABASE.WebUser();
                            dal_web_user_role = new _EPAM_DALDATABASE.WebUserRole();
                        }

                        break;
                }
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public IEnumerable<WebUserRoleDTO> GetAll()
        {
            try
            {
                return dal_web_user_role.GetAll().ToArray();
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        
        public bool Create(WebUserRoleDTO note)
        {
            try
            {
                return dal_web_user_role.Create(note);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Delete(WebUserRoleDTO note)
        {
            try
            {
                return dal_web_user_role.Delete(note);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Update(WebUserRoleDTO note)
        {
            try
            {
                dal_web_user_role.Update(note);
                return true;
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public WebUserRoleDTO Get(int id)
        {
            try
            {
                return dal_web_user_role.Get(id);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool IsWebUserInRole(string name, string role)
        {
            try
            {
                return dal_web_user_role.IsWebUserInRole(name, role);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

       public string[] GetRolesForUser(string web_user_name)
        {

            try
            {
                return dal_web_user_role.GetRolesForUser(web_user_name);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
    }
}
  