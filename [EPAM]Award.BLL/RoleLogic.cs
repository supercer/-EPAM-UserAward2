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
namespace _EPAM_BLL
{
    public class RoleLogic: IRoleBLL
    {
        private _EPAM_Intefases.DAL.IRoleDAL dal;

        public RoleLogic()
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
                            dal = new _EPAM_DALDATABASE.Role();
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

        public bool Create(RoleDTO note)
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

        public bool Delete(int id)
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

        public RoleDTO Get(int id)
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

        public IEnumerable<RoleDTO> GetAll()
        {
            return dal.GetAll().ToArray();
        }

        public int GetIdAnRoleName(string role_name)
        {
            try
            {
                return dal.GetIdAnRoleName(role_name);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool Update(RoleDTO note)
        {
            try
            {
                dal.Update(note);
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
