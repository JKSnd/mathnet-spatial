using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Spatial.Serialization.Xml.UnitTests
{
    public class XmlSerializableDummy : IXmlSerializable
    {
        private readonly string _name;
        private XmlSerializableDummy()
        {

        }
        public XmlSerializableDummy(string name, int age)
        {
            this.Age = age;
            this._name = name;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public int Age { get; set; }

        public XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            var e = (XElement)XNode.ReadFrom(reader);
            this.Age = XmlConvert.ToInt32(e.Attribute("Age").Value);
            var name = e.ReadAttributeOrElement("Name");
            XmlExt.WriteValueToReadonlyField(this, name, () => this._name);
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Age", this.Age.ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("Name", this.Name);
        }
    }
}