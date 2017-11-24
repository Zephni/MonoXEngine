using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Xml.XPath;

namespace MonoXEngine
{
    public class DataSet
    {
        protected Dictionary<string[], object> Data;

        public DataSet()
        {
            this.Data = new Dictionary<string[], object>(new StringArrayComparer());
        }

        public void Set(string[] keys, object value)
        {
            this.Data.Add(keys, value);
        }

        public T Get<T>(string[] keys)
        {
            return (T)Convert.ChangeType(this.Data[keys], typeof(T));
        }

        public XDocument ToXML(string rootName = "Root")
        {
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            xmlDocument.Add(new XElement(rootName));

            foreach (KeyValuePair<string[], object> item in this.Data)
            {
                List<string> keys = new List<string>() { rootName }.Concat(item.Key.ToList()).ToList();
                for (int I = 0; I < keys.Count; I++)
                    if (xmlDocument.XPathSelectElement(string.Join("/", keys.GetRange(0, I + 1))) == null)
                        xmlDocument.XPathSelectElement(string.Join("/", keys.GetRange(0, I))).Add(new XElement(keys[I], ((I < item.Key.Length)) ? null : item.Value));
            }

            return xmlDocument;
        }

        public void FromXML(XDocument xmlDocument)
        {
            foreach(XElement xElement in xmlDocument.Root.Descendants())
            {
                if(!xElement.HasElements)
                {
                    List<string> keyList = new List<string>();
                    foreach (XElement parent in new List<XElement>() { xElement }.Concat(xElement.Ancestors()))
                        keyList.Add(parent.Name.ToString());                        

                    keyList.RemoveAt(keyList.Count-1);
                    keyList.Reverse();
                    this.Set(keyList.ToArray(), xElement.Value);
                }
            }
        }
    }

    public class StringArrayComparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[] x, string[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(string[] strarr)
        {
            return string.Join("|", strarr).GetHashCode();
        }
    }
}
