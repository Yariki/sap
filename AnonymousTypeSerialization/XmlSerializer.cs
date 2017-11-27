using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml;
using System.Xml.Linq;

namespace AnonymousTypeSerialization
{
    public static class XmlSerializer
    {
        private static List<Type> AllowedType = new List<Type>(){typeof(string)};

        public static string SerializeToXml(this object input, string name)
        {
            return ToXml(input, name).ToString();
        }

        private static bool IsAllowedType(this Type type)
        {
            return type.IsPrimitive || AllowedType.Contains(type);
        }

        private static XElement ToXml(object input, string name)
        {
            var type = input.GetType();
            var properties = type.GetProperties();

            var root = new XElement(XmlConvert.EncodeName(name));

            var elements = from p in properties
                let n = XmlConvert.EncodeName(p.Name)
                let v = p.GetValue(input, null)
                let val = p.PropertyType.IsAllowedType()
                    ? new XElement(p.Name, p.GetValue(input))
                    : ToXml(p.GetValue(input), p.Name)
                where val != null
                select val;

            root.Add(elements);
            return root;
        }

    }
}