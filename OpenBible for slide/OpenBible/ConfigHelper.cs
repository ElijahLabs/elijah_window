using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpenBible
{
    public class ConfigHelper
    {
        private XmlDocument xDoc;
        private XmlNode xNode;
        private XmlElement xElemUID;

        public ConfigHelper()
        {
            xDoc = new XmlDocument();
            xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            xNode = xDoc.SelectSingleNode("//OpenBible.Properties.Settings");
        }

        public void Write(string name, string value)
        {
            xElemUID = (XmlElement)xNode.SelectSingleNode("//setting[@name='" + name + "']").FirstChild;
            xElemUID.InnerText = value;
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

    }
}
