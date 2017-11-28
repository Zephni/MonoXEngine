using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using System.Collections;

namespace MonoXEngine
{
    public class DataSet
    {
        public Dictionary<string, object> Data;

        public DataSet()
        {
            this.Data = new Dictionary<string, object>();
        }

        public string KeysToString(List<string> keys, string seperator = "/")
        {
            return string.Join("/", keys);
        }

        public List<string> StringToKeys(string keys, string seperator = "/")
        {
            return keys.Split(seperator.ToCharArray()).ToList<string>();
        }

        public void Set(List<string> keys, object value)
        {
            this.Data.Add(KeysToString(keys), value);
        }

        public void Set(string keys, object value)
        {
            this.Data.Add(keys, value);
        }

        public T Get<T>(List<string> keys)
        {
            return (T)Convert.ChangeType(Data[KeysToString(keys)], typeof(T));
        }

        public T Get<T>(params string[] keys)
        {
            return Get<T>(keys.ToList());
        }

        public Dictionary<string, object> GetGroup(string path)
        {
            List<string> pathKeys = path.Split('/').ToList();
            Dictionary<string, object> newDict = new Dictionary<string, object>();

            foreach(KeyValuePair<string, object> item in Data)
            {
                List<string> keys = item.Key.Split('/').ToList();
                if(keys.GetRange(0, pathKeys.Count).SequenceEqual(pathKeys))
                {
                    if(pathKeys.Count < keys.Count)
                        newDict.Add(keys[pathKeys.Count], item.Value);
                }
            }

            return newDict;
        }

        public void FromXML(XDocument xDocument)
        {
            foreach(XElement xEl in xDocument.Root.Descendants().Where(x => x.Descendants().Count() == 0))
            {
                List<string> keys = new List<string>();
                foreach (XElement a in xEl.AncestorsAndSelf().ToList())
                    keys.Add(a.Name.ToString());

                keys.Reverse();
                keys.RemoveAt(0);
                Set(string.Join("/", keys), xEl.Value);
            }
        }

        public XDocument ToXML()
        {
            XDocument xDocument = new XDocument();
            xDocument.Add(new XElement("Root"));

            foreach(KeyValuePair<string, object> kv in Data)
            {

            }

            return xDocument;
        }
    }
}
