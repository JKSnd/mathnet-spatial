using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Units;

namespace Spatial.Serialization.Xml.UnitTests.Units
{
  public class AngleDtoTests
    {
        private const double Tolerance = 1e-6;

        [TestCase("15 °", @"<Angle Value=""0.26179938779914941"" />")]
        public void XmlTest(string vs, string xml)
        {
            var angle = new AngleDto();
            AssertXml.XmlRoundTrips(angle, xml, (e, a) =>
            {
                Assert.AreEqual(e.Radians, a.Radians, Tolerance);
                Assert.IsInstanceOf<AngleDto>(a);
            });
            var serializer = new XmlSerializer(typeof(AngleDto));
            using (var reader = new StringReader(@"<Angle><Value>0.261799387799149</Value></Angle>"))
            {
                var fromElements = (AngleDto)serializer.Deserialize(reader);
                Assert.AreEqual(angle.Radians, fromElements.Radians, Tolerance);
            }
        }
    }
}
