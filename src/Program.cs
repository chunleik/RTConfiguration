using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            while (true)
            {
                Console.WriteLine(DateTime.Now+"  "+loadedRtSection.RtSection.Property1);
                Thread.Sleep(500);
            }

            //Console.ReadKey();
        }
    }
}
