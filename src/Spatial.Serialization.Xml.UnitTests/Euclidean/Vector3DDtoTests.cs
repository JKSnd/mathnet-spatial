using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class Vector3DDtoTests
    {
        [Test]
        public void SerializeDeserialize()
        {
            var v = new Vector3DDto(1, -2, 3);
            const string Xml = @"<Vector3D X=""1"" Y=""-2"" Z=""3"" />";
            const string ElementXml = @"<Vector3D><X>1</X><Y>-2</Y><Z>3</Z></Vector3D>";
            var roundTrip = AssertXml.XmlSerializerRoundTrip(v, Xml);
            AssertDtoGeometry.AreEqual(v, roundTrip);

            var serializer = new XmlSerializer(typeof(Vector3DDto));

            var actuals = new[]
                          {
                              Vector3DDto.ReadFrom(XmlReader.Create(new StringReader(Xml))),
                              Vector3DDto.ReadFrom(XmlReader.Create(new StringReader(ElementXml))),
                              (Vector3DDto)serializer.Deserialize(new StringReader(Xml)),
                              (Vector3DDto)serializer.Deserialize(new StringReader(ElementXml))
                          };
            foreach (var actual in actuals)
            {
                AssertDtoGeometry.AreEqual(v, actual);
            }
        }
    }
}
