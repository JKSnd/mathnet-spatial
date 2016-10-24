using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class UnitVector3DDtoTests
    {
        [Test]
        public void XmlRoundTrips()
        {
            var uv = new UnitVector3DDto(0.2672612419124244, -0.53452248382484879, 0.80178372573727319);
            var xml = @"<UnitVector3D X=""0.2672612419124244"" Y=""-0.53452248382484879"" Z=""0.80178372573727319"" />";
            var elementXml = @"<UnitVector3D><X>0.2672612419124244</X><Y>-0.53452248382484879</Y><Z>0.80178372573727319</Z></UnitVector3D>";

            AssertXml.XmlRoundTrips(uv, xml, (e, a) => AssertDtoGeometry.AreEqual(e, a));
            var serializer = new XmlSerializer(typeof(UnitVector3DDto));
            var actuals = new[]
                                {
                                    UnitVector3DDto.ReadFrom(XmlReader.Create(new StringReader(xml))),
                                    (UnitVector3DDto)serializer.Deserialize(new StringReader(xml)),
                                    (UnitVector3DDto)serializer.Deserialize(new StringReader(elementXml))
                                };
            foreach (var actual in actuals)
            {
                AssertDtoGeometry.AreEqual(uv, actual);
            }
        }
    }
}
