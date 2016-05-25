﻿using _EPAM_Intefases.BLL;
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
   public class WebUserLogic: IWebUserBLL
    {
        private _EPAM_Intefases.DAL.IWebUserDAL dal;
        
        public WebUserLogic()
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
                            dal = new _EPAM_DALDATABASE.WebUser();
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

        public bool Create(WebUserDTO note)
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

        public WebUserDTO Get(int id)
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

        public IEnumerable<WebUserDTO> GetAll()
        {
            return dal.GetAll().ToArray();
        }

        public int GetIdAnWebUserName(string web_user_name)
        {
            try
            {
               return dal.GetIdAnWebUserName(web_user_name);            
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }

        public bool IsWebUser(string name, string password)
        {
            return dal.IsWebUser(name, password);
        }

        public bool Update(WebUserDTO note)
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