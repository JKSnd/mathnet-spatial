using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests.Euclidean
{
    [TestFixture]
    public class Point3DDtoTests
    {
        [Test]
        public void XmlRoundtrip()
        {
            var p = new Point3DDto(1, -2, 3);
            const string Xml = @"<Point3D X=""1"" Y=""-2"" Z=""3"" />";
            const string ElementXml = @"<Point3D><X>1</X><Y>-2</Y><Z>3</Z></Point3D>";
            AssertXml.XmlRoundTrips(p, Xml, (expected, actual) => AssertDtoGeometry.AreEqual(expected, actual));
            var serializer = new XmlSerializer(typeof (Point3DDto));

            var actuals = new[]
                          {
                              Point3DDto.ReadFrom(XmlReader.Create(new StringReader(Xml))),
                              Point3DDto.ReadFrom(XmlReader.Create(new StringReader(ElementXml))),
                              (Point3DDto)serializer.Deserialize(new StringReader(Xml)),
                              (Point3DDto)serializer.Deserialize(new StringReader(ElementXml))
                          };
            foreach (var actual in actuals)
            {
                AssertDtoGeometry.AreEqual(p, actual);
            }
        }
    }
}
