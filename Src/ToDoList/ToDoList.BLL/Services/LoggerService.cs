using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BLL.Entities;


namespace ToDoList.BLL.Services
{
    public class LoggerService
    {
        public LoggerService() { Logger.InitLogger(); }

        public void LogInfo(string msg)
        {
            Logger.Log.Info(msg);
        }

        public void LogError(string msg)
        {
            Logger.Log.Error(msg);
        }
    }
}
