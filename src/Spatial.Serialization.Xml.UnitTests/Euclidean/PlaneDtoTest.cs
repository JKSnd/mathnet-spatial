using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class PlaneDtoTest
    {
        [TestCase("p:{0, 0, 0} v:{0, 0, 1}", @"<Plane><RootPoint X=""0"" Y=""0"" Z=""0"" /><Normal X=""0"" Y=""0"" Z=""1"" /></Plane>")]
        public void XmlRoundTrips(string p1s, string xml)
        {
            var plane = PlaneDto.Parse(p1s);
            AssertXml.XmlRoundTrips(plane, xml, (e, a) => AssertDtoGeometry.AreEqual(e, a));
        }
    }
}
