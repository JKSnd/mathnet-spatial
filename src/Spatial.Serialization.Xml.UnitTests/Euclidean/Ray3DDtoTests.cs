using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class Ray3DDtoTests
    {
        [TestCase("1, 2, 3", "-1, 2, 3", false, @"<Ray3D><ThroughPoint X=""1"" Y=""2"" Z=""3"" /><Direction X=""-0.2672612419124244"" Y=""0.53452248382484879"" Z=""0.80178372573727319"" /></Ray3D>")]
        public void XmlTests(string ps, string vs, bool asElements, string xml)
        {
            var ray = new Ray3DDto(Point3DDto.Parse(ps), UnitVector3DDto.Parse(vs));
            AssertXml.XmlRoundTrips(ray, xml, (e, a) => AssertDtoGeometry.AreEqual(e, a, 1e-6));
        }
    }
}
