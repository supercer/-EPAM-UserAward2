
namespace _EPAM_BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_Entites;
    using _EPAM_Intefases.BLL;
    using System.IO;
    using System.Configuration;
    using _EPAM_Intefases.DAL;
    public class AwardLogic : IAwardBLL
    {
        private IAwardDAL dal;
        private _EPAM_Intefases.DAL.IimageDAL dal_image;
        public AwardLogic()
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
                            string way = Environment.CurrentDirectory + @"\LogAward.txt";
                            dal = new _EPAM_DALFile.DALFileAward();
                        }

                        break;
                    case "Collection":
                        {
                            dal = new _EPAM_DALCollection.DALAward();
                        }

                        break;
                    case "DATABASE":
                        {
                            dal = new _EPAM_DALDATABASE.Award();
                            dal_image = new _EPAM_DALDATABASE.Image();
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

        public bool Create(AwardDTO note)
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

        public AwardDTO Get(Guid id)
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

        public IEnumerable<AwardDTO> GetAll()
        {
            return dal.GetAll().ToArray();
        }

        public bool Update(AwardDTO user)
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
        public bool SetImageToUser(Guid award_id, int image_id)
        {
            AwardDTO award;
            try
            {
                award = dal.Get(award_id);
                award.ImageId = image_id;
                return dal.Update(award);
            }

            catch (Exception e)
            {
                Logger.Logger.WriteLog(e);
                throw e;
            }
        }
    }
}
