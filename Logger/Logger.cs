
namespace Logger
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Logger
    {
        public static List<Exception> Exceptions;
        public static FileInfo file = new FileInfo(ConfigurationManager.AppSettings["DataBaseLogger"]);
        static Logger()
        {
            try
            { 
            file = new FileInfo(ConfigurationManager.AppSettings["DataBaseLogger"]);
            if (!file.Exists)
            {
                file.Create();
            }
        }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void WriteLog(Exception e)
        {
            try
            {
                Exceptions.Add(e);
                using (StreamWriter writer = new StreamWriter(file.FullName))
                {
                    writer.WriteLine("e");
                    writer.WriteLine(e.Data);
                    writer.WriteLine(e.HResult);
                    writer.WriteLine(e.InnerException);
                    writer.WriteLine(e.Message);
                    writer.WriteLine(e.Source);
                    writer.WriteLine(e.StackTrace);
                }
            }
            catch (Exception f)
            {
                throw f;
            }
            
        }


    }
}
