﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MathNet.Spatial.Euclidean;
using NUnit.Framework;

namespace MathNet.Spatial.UnitTests.Euclidean
{
    [TestFixture]
    public class Ray3DTests
    {
        [TestCase("p:{1, 2, 3} v:{0, 0, 1}","1, 2, 3", "0, 0, 1")]
        public void Parse(string rs, string eps, string evs)
        {
            var ray = Ray3D.Parse(rs);
            AssertGeometry.AreEqual(Point3D.Parse(eps), ray.ThroughPoint);
            AssertGeometry.AreEqual(Vector3D.Parse(evs), ray.Direction);
        }

        [TestCase("p:{0, 0, 0} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 1, 0}", "0, 0, 0", "-1, 0, 0")]
        [TestCase("p:{0, 0, 2} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 1, 0}", "0, 0, 2", "-1, 0, 0")]
        public void IntersectionOf(string pl1s, string pl2s, string eps, string evs)
        {
            var plane1 = Plane.Parse(pl1s);
            var plane2 = Plane.Parse(pl2s);
            var actual = Ray3D.IntersectionOf(plane1, plane2);
            var expected = Ray3D.Parse(eps, evs);
            AssertGeometry.AreEqual(expected, actual);
        }

        [Test]
        public void LineToTest()
        {
            var ray = new Ray3D(new Point3D(0, 0, 0), UnitVector3D.ZAxis);
            var point3D = new Point3D(1, 0, 0);
            Line3D line3DTo = ray.LineTo(point3D);
            AssertGeometry.AreEqual(new Point3D(0, 0, 0), line3DTo.StartPoint);
            AssertGeometry.AreEqual(point3D, line3DTo.EndPoint, float.Epsilon);
        }

        [TestCase("0, 0, 0", "1, -1, 1", "0, 0, 0", "1, -1, 1", true)]
        [TestCase("0, 0, 2", "1, -1, 1", "0, 0, 0", "1, -1, 1", false)]
        [TestCase("0, 0, 0", "1, -1, 1", "0, 0, 0", "2, -1, 1", false)]
        public void Equals(string p1s, string v1s, string p2s, string v2s, bool expected)
        {
            var ray1 = Ray3D.Parse(p1s, v1s);
            var ray2 = Ray3D.Parse(p2s, v2s);
            Assert.AreEqual(expected, ray1.Equals(ray2));
            Assert.AreEqual(expected, ray1 == ray2);
            Assert.AreEqual(!expected, ray1 != ray2);
        }
    
        [Test]
        public void BinaryRountrip()
        {
            var v = new Ray3D(new Point3D(1, 2, -3), new UnitVector3D(1, 2, 3));
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, v);
                ms.Flush();
                ms.Position = 0;
                var roundTrip = (Ray3D)formatter.Deserialize(ms);
                AssertGeometry.AreEqual(v, roundTrip);
            }
        }
    }
}
