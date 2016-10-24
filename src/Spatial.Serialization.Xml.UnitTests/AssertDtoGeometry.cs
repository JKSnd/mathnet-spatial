using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;
using Spatial.Serialization.Xml.Euclidean;

namespace Spatial.Serialization.Xml.UnitTests
{
    public static class AssertDtoGeometry
    {
        public static void AreEqual(CoordinateSystemDto CoordinateSystemDto, Point3DDto origin, Vector3DDto xAxis, Vector3DDto yAxis, Vector3DDto zAxis, double tolerance = 1e-6)
        {
            AreEqual(xAxis, CoordinateSystemDto.XAxis, tolerance);
            AreEqual(yAxis, CoordinateSystemDto.YAxis, tolerance);
            AreEqual(zAxis, CoordinateSystemDto.ZAxis, tolerance);
            AreEqual(origin, CoordinateSystemDto.Origin, tolerance);

            AssertDtoGeometry.AreEqual(new double[] { xAxis.X, xAxis.Y, xAxis.Z, 0 }, CoordinateSystemDto.Column(0).ToArray(), tolerance);
            AssertDtoGeometry.AreEqual(new double[] { yAxis.X, yAxis.Y, yAxis.Z, 0 }, CoordinateSystemDto.Column(1).ToArray(), tolerance);
            AssertDtoGeometry.AreEqual(new double[] { zAxis.X, zAxis.Y, zAxis.Z, 0 }, CoordinateSystemDto.Column(2).ToArray(), tolerance);
            AssertDtoGeometry.AreEqual(new double[] { origin.X, origin.Y, origin.Z, 1 }, CoordinateSystemDto.Column(3).ToArray(), tolerance);
        }

        public static void AreEqual(UnitVector3DDto expected, UnitVector3DDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            Assert.AreEqual(expected.X, actual.X, tolerance, message);
            Assert.AreEqual(expected.Y, actual.Y, tolerance, message);
            Assert.AreEqual(expected.Z, actual.Z, tolerance, message);
        }

        public static void AreEqual(Vector3DDto expected, Vector3DDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            Assert.AreEqual(expected.X, actual.X, tolerance, message);
            Assert.AreEqual(expected.Y, actual.Y, tolerance, message);
            Assert.AreEqual(expected.Z, actual.Z, tolerance, message);
        }

        public static void AreEqual(UnitVector3DDto expected, Vector3DDto actual, double tolerance = 1e-6, string message = "")
        {
            AreEqual(expected.ToVector3DDto(), actual, tolerance, message);
        }

        public static void AreEqual(Vector3DDto expected, UnitVector3DDto actual, double tolerance = 1e-6, string message = "")
        {
            AreEqual(expected, actual.ToVector3DDto(), tolerance, message);
        }

        public static void AreEqual(Vector2DDto expected, Vector2DDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            Assert.AreEqual(expected.X, actual.X, tolerance, message);
            Assert.AreEqual(expected.Y, actual.Y, tolerance, message);
        }

        public static void AreEqual(Point3DDto expected, Point3DDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            Assert.AreEqual(expected.X, actual.X, tolerance, message);
            Assert.AreEqual(expected.Y, actual.Y, tolerance, message);
            Assert.AreEqual(expected.Z, actual.Z, tolerance, message);
        }

        public static void AreEqual(CoordinateSystemDto expected, CoordinateSystemDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            if (expected.Values.Length != actual.Values.Length)
                Assert.Fail();
            for (int i = 0; i < expected.Values.Length; i++)
            {
                Assert.AreEqual(expected.Values[i], actual.Values[i], tolerance);
            }
        }

        public static void AreEqual(double[] expected, double[] actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", "{" + string.Join(",", expected) + "}", "{" + string.Join(",", actual) + "}");
            if (expected.Length != actual.Length)
                Assert.Fail();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], tolerance);
            }
        }

        public static void AreEqual(Line3DDto expected, Line3DDto actual, double tolerance = 1e-6)
        {
            AreEqual(expected.StartPoint, actual.StartPoint, tolerance);
            AreEqual(expected.EndPoint, actual.EndPoint, tolerance);
        }

        public static void AreEqual(Ray3DDto expected, Ray3DDto actual, double tolerance = 1e-6, string message = "")
        {
            AreEqual(expected.ThroughPoint, actual.ThroughPoint, tolerance, message);
            AreEqual(expected.Direction, actual.Direction, tolerance, message);
        }

        public static void AreEqual(PlaneDto expected, PlaneDto actual, double tolerance = 1e-6, string message = "")
        {
            AreEqual(expected.Normal, actual.Normal, tolerance, message);
            AreEqual(expected.RootPoint, actual.RootPoint, tolerance, message);
        }

        public static void AreEqual(Matrix<double> expected, Matrix<double> actual, double tolerance = 1e-6)
        {
            Assert.AreEqual(expected.RowCount, actual.RowCount);
            Assert.AreEqual(expected.ColumnCount, actual.ColumnCount);
            double[] expectedRowWiseArray = expected.ToRowWiseArray();
            double[] actualRowWiseArray = actual.ToRowWiseArray();
            for (int i = 0; i < expectedRowWiseArray.Length; i++)
            {
                Assert.AreEqual(expectedRowWiseArray[i], actualRowWiseArray[i], tolerance);
            }
        }

        public static void AreEqual(Point2DDto expected, Point2DDto actual, double tolerance = 1e-6, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = string.Format("Expected {0} but was {1}", expected, actual);
            Assert.AreEqual(expected.X, actual.X, tolerance, message);
            Assert.AreEqual(expected.Y, actual.Y, tolerance, message);
        }
    }
}