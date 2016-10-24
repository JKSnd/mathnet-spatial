using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class Line3DDtoTests
    {
        [TestCase("1, 2, 3", "4, 5, 6", @"<Line3D><StartPoint X=""1"" Y=""2"" Z=""3"" /><EndPoint X=""4"" Y=""5"" Z=""6"" /></Line3D>")]
        public void XmlTests(string p1s, string p2s, string xml)
        {
            var p1 = Point3DDto.Parse(p1s);
            var p2 = Point3DDto.Parse(p2s);
            var l = new Line3DDto(p1, p2);
            AssertXml.XmlRoundTrips(l, xml, (e, a) => AssertDtoGeometry.AreEqual(e, a));
        }
    
    }
}
