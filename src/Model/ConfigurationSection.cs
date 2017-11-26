using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RTConfiguration.Model
{
    public class ConfigurationSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var s = section.OuterXml;
            var obj = Util.XmlSerializerHelper.ToInstance<Model.RtSection>(s);
            return obj;
        }
    }
}
