using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    public class Point2DDtoTests
    {
        [Test]
        public void XmlRountrip()
        {
            var p = new Point2DDto(1, 2);
            const string Xml = @"<Point2D X=""1"" Y=""2"" />";
            const string ElementXml = @"<Point2D><X>1</X><Y>2</Y></Point2D>";

            AssertXml.XmlRoundTrips(p, Xml, (e, a) => AssertDtoGeometry.AreEqual(e, a));
            var serializer = new XmlSerializer(typeof(Point2DDto));

            var actuals = new[]
                          {
                              Point2DDto.ReadFrom(XmlReader.Create(new StringReader(Xml))),
                              Point2DDto.ReadFrom(XmlReader.Create(new StringReader(ElementXml))),
                              (Point2DDto)serializer.Deserialize(new StringReader(Xml)),
                              (Point2DDto)serializer.Deserialize(new StringReader(ElementXml))
                          };
            foreach (var actual in actuals)
            {
                AssertDtoGeometry.AreEqual(p, actual);
            }
        }
    }
}
