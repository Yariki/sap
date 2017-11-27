using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Json;

namespace AnonymousTypeSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj1 = new
            {
                M = "StringValue",
                I = 10,
                AnotherObject = new
                {
                    B = "StringValue",
                    D = 10.2
                }
            };

            var json = JsonParser.Serialize(obj1);
            Console.WriteLine(json);

            var xml = obj1.SerializeToXml("Root");
            Console.WriteLine(xml);

            Console.ReadLine();

        }
    }
}
