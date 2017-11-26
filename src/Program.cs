using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            //var s = new Model.RtSection();
            //s.Property1 = "Sample";
            //s.Property2 = 2;
            //Util.XmlSerializerHelper.SaveToXml<Model.RtSection>("RtSection.txt", s);
            //var section = System.Configuration.ConfigurationManager.GetSection("RtSection") as Model.RtSection;
            //Console.WriteLine(section.Property1);
            //Console.WriteLine(section.Property2);
            var loadedRtSection = new Model.LoadedRtSection();
            loadedRtSection.Load();
            Console.WriteLine(loadedRtSection.RtSection.Property1);
            Console.WriteLine(loadedRtSection.RtSection.Property2);

            Console.ReadKey();
        }
    }
}
