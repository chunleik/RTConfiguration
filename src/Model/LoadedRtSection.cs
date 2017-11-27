using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace RTConfiguration.Model
{
    public class LoadedRtSection
    {
        private const string rtSectionFilePath = "RtSection.txt";
        private string _readFileName;
        private Timer timer;
        private RtSection _rtSection;
        private FileSystemWatcher fileSystemWatcher;
        private object lockObj = new object();

        public RtSection RtSection
        {
            get { return _rtSection; }
        }

        public void Load()
        {
            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Dispose();
                fileSystemWatcher = null;
            }

            RtSection rtSection = null;

            //首先读取配置文件
            rtSection = ConfigurationManager.GetSection("RtSection") as RtSection;
            _readFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (rtSection == null)
            {
                //在工作目录读取RtSection.txt文件
                rtSection = Util.XmlSerializerHelper.LoadFromXml<RtSection>(rtSectionFilePath);
                _readFileName = Path.Combine(Directory.GetCurrentDirectory(), rtSectionFilePath);
                if (rtSection == null)
                {
                    throw new Exception("Cannot get the RtSection");
                }
            }

            if (rtSection != null)
            {
                _rtSection = rtSection;
            }

            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = Path.GetDirectoryName(_readFileName);
            fileSystemWatcher.Filter = Path.GetFileName(_readFileName);
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Created += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Changed;
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //晚一秒钟通知刷新，以防止可能出现的多次触发
            lock (lockObj)
            {
                if (timer == null)
                {
                    timer = new Timer(Reload, null, 1000, Timeout.Infinite);
                }
                else
                {
                    timer.Change(1000, Timeout.Infinite);
                }
            }
        }

        private void Reload(object state)
        {
            Load();
        }
    }
}
