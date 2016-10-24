using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class CoordinateSystemDtoTest
    {
        [Test]
        public void XmlRoundTrips()
        {
            var cs = new CoordinateSystemDto(new Point3DDto(1, -2, 3), new Vector3DDto(0, 1, 0), new Vector3DDto(0, 0, 1), new Vector3DDto(1, 0, 0));
            const string expected = @"
<CoordinateSystem>
    <Origin X=""1"" Y=""-2"" Z=""3"" />
    <XAxis X=""0"" Y=""1"" Z=""0"" />
    <YAxis X=""0"" Y=""0"" Z=""1"" />
    <ZAxis X=""1"" Y=""0"" Z=""0"" />
</CoordinateSystem>";
            AssertXml.XmlRoundTrips(cs, expected, (e, a) => AssertDtoGeometry.AreEqual(e, a));
        }
    }
}
