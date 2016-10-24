using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    public class Vector2DDtoTests
    {
        [Test]
        public void XmlRoundtrip()
        {
            const string Xml = @"<Vector2D X=""1"" Y=""2"" />";
            const string ElementXml = @"<Vector2D><X>1</X><Y>2</Y></Vector2D>";
            var v = new Vector2DDto(1,2);

            AssertXml.XmlRoundTrips(v, Xml, (e, a) => AssertDtoGeometry.AreEqual(e, a));

            var serializer = new XmlSerializer(typeof(Vector2DDto));


            var actuals = new[]
                          {
                              Vector2DDto.ReadFrom(XmlReader.Create(new StringReader(Xml))),
                              Vector2DDto.ReadFrom(XmlReader.Create(new StringReader(ElementXml))),
                              (Vector2DDto)serializer.Deserialize(new StringReader(Xml)),
                              (Vector2DDto)serializer.Deserialize(new StringReader(ElementXml))
                          };
            foreach (var actual in actuals)
            {
               AssertDtoGeometry.AreEqual(v, actual);
            }
        }
    }
}
