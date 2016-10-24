using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Spatial.Serialization.Xml.Units;

namespace Spatial.Serialization.Xml.Euclidean
{
    [Serializable]
    public class CoordinateSystemDto : DenseMatrix, IEquatable<CoordinateSystemDto>, IXmlSerializable
    {
        static string _item3DPattern = Parser.Vector3DPattern.Trim('^', '$');

        public static readonly string CsPattern = string.Format(@"^ *o: *{{(?<op>{0})}} *x: *{{(?<xv>{0})}} *y: *{{(?<yv>{0})}} *z: *{{(?<zv>{0})}} *$", _item3DPattern);

    

        public CoordinateSystemDto(Point3DDto origin, Vector3DDto xAxis, Vector3DDto yAxis, Vector3DDto zAxis)
            : base(4)
        {
            this.SetColumn(0, new[] { xAxis.X, xAxis.Y, xAxis.Z, 0 });
            this.SetColumn(1, new[] { yAxis.X, yAxis.Y, yAxis.Z, 0 });
            this.SetColumn(2, new[] { zAxis.X, zAxis.Y, zAxis.Z, 0 });
            this.SetColumn(3, new[] { origin.X, origin.Y, origin.Z, 1 });
        }

        public CoordinateSystemDto(Matrix<double> matrix)
            : base(4, 4, matrix.ToColumnWiseArray())
        {
            if (matrix.RowCount != 4)
            {
                throw new ArgumentException("Rowcount must be 4");
            }

            if (matrix.ColumnCount != 4)
            {
                throw new ArgumentException("Rowcount must be 4");
            }
        }

        public Vector3DDto XAxis
        {
            get { return new Vector3DDto(this.SubMatrix(0, 3, 0, 1).ToRowWiseArray()); }
        }

        public Vector3DDto YAxis
        {
            get { return new Vector3DDto(this.SubMatrix(0, 3, 1, 1).ToRowWiseArray()); }
        }

        public Vector3DDto ZAxis
        {
            get { return new Vector3DDto(this.SubMatrix(0, 3, 2, 1).ToRowWiseArray()); }
        }

        public Point3DDto Origin
        {
            get { return new Point3DDto(this.SubMatrix(0, 3, 3, 1).ToRowWiseArray()); }
        }


        public bool Equals(CoordinateSystemDto other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.Values.Length != this.Values.Length)
            {
                return false;
            }

            return !this.Values.Where((t, i) => Math.Abs(other.Values[i] - t) > 1E-15).Any();
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(CoordinateSystemDto))
            {
                return false;
            }

            return this.Equals((CoordinateSystemDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.XAxis.GetHashCode();
                result = (result * 397) ^ this.YAxis.GetHashCode();
                result = (result * 397) ^ this.ZAxis.GetHashCode();
                return result;
            }
        }

        public new string ToString()
        {
            return string.Format("Origin: {0}, XAxis: {1}, YAxis: {2}, ZAxis: {3}", this.Origin, this.XAxis, this.YAxis, this.ZAxis);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var e = (XElement)XNode.ReadFrom(reader);

            var xAxis = new Vector3DDto(double.NaN, double.NaN, double.NaN);
            xAxis.ReadXml(e.SingleElementReader("XAxis"));
            this.SetColumn(0, new[] { xAxis.X, xAxis.Y, xAxis.Z, 0 });

            var yAxis = new Vector3DDto(double.NaN, double.NaN, double.NaN);
            yAxis.ReadXml(e.SingleElementReader("YAxis"));
            this.SetColumn(1, new[] { yAxis.X, yAxis.Y, yAxis.Z, 0 });

            var zAxis = new Vector3DDto(double.NaN, double.NaN, double.NaN);
            zAxis.ReadXml(e.SingleElementReader("ZAxis"));
            this.SetColumn(2, new[] { zAxis.X, zAxis.Y, zAxis.Z, 0 });

            var origin = new Point3DDto(double.NaN, double.NaN, double.NaN);
            origin.ReadXml(e.SingleElementReader("Origin"));
            this.SetColumn(3, new[] { origin.X, origin.Y, origin.Z, 1 });
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement("Origin", this.Origin);
            writer.WriteElement("XAxis", this.XAxis);
            writer.WriteElement("YAxis", this.YAxis);
            writer.WriteElement("ZAxis", this.ZAxis);
        }
    }
}
