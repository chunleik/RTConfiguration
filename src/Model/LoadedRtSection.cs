using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTConfiguration.Model
{
    public class LoadedRtSection
    {
        private string _rtSectionFilePath = "RtSection.txt";

        private RtSection _rtSection;

        public RtSection RtSection
        {
            get { return _rtSection; }
        }

        public void Load()
        {
            _rtSection = null;
            //首先读取配置文件
            _rtSection = ConfigurationManager.GetSection("RtSection") as RtSection;
            if (_rtSection != null)
                return;
            //在工作目录读取RtSection.txt文件
            _rtSection = Util.XmlSerializerHelper.LoadFromXml<RtSection>(_rtSectionFilePath);
            if (_rtSection == null)
            {
                throw new Exception("Cannot get the RtSection");
            }
        }
    }
}
