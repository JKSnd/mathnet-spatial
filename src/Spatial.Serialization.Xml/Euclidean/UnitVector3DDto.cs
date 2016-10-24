using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Spatial.Serialization.Xml.Euclidean
{
    /// <summary>
    /// A unit vector, this is used to describe a direction in 3D
    /// </summary>
    [Serializable]
    public struct UnitVector3DDto : IXmlSerializable, IEquatable<UnitVector3DDto>, IEquatable<Vector3DDto>, IFormattable
    {
        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double X;

        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double Z;

        public UnitVector3DDto(double x, double y, double z)
        {
            var l = Math.Sqrt((x*x) + (y*y) + (z*z));
            if (l < float.Epsilon)
            {
                throw new ArgumentException("l < float.Epsilon");
            }

            this.X = x/l;
            this.Y = y/l;
            this.Z = z/l;
        }

        public UnitVector3DDto(IEnumerable<double> data)
            : this(data.ToArray())
        {
        }

        public UnitVector3DDto(double[] data)
            : this(data[0], data[1], data[2])
        {
            if (data.Length != 3)
            {
                throw new ArgumentException("Size must be 3");
            }
        }


      
        public override string ToString()
        {
            return this.ToString(null, CultureInfo.InvariantCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            var numberFormatInfo = provider != null ? NumberFormatInfo.GetInstance(provider) : CultureInfo.InvariantCulture.NumberFormat;
            string separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
            return string.Format("({0}{1} {2}{1} {3})", this.X.ToString(format, numberFormatInfo), separator, this.Y.ToString(format, numberFormatInfo), this.Z.ToString(format, numberFormatInfo));
        }

        public bool Equals(Vector3DDto other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        public bool Equals(UnitVector3DDto other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        public bool Equals(UnitVector3DDto other, double tolerance)
        {
            if (tolerance < 0)
            {
                throw new ArgumentException("epsilon < 0");
            }

            return Math.Abs(other.X - this.X) < tolerance &&
                   Math.Abs(other.Y - this.Y) < tolerance &&
                   Math.Abs(other.Z - this.Z) < tolerance;
        }

        public bool Equals(Vector3DDto other, double tolerance)
        {
            if (tolerance < 0)
            {
                throw new ArgumentException("epsilon < 0");
            }

            return Math.Abs(other.X - this.X) < tolerance &&
                   Math.Abs(other.Y - this.Y) < tolerance &&
                   Math.Abs(other.Z - this.Z) < tolerance;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            return (obj is UnitVector3DDto && this.Equals((UnitVector3DDto)obj)) ||
                   (obj is Vector3DDto && this.Equals((Vector3DDto)obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X.GetHashCode();
                hashCode = (hashCode*397) ^ this.Y.GetHashCode();
                hashCode = (hashCode*397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized. </param>
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var e = (XElement)XNode.ReadFrom(reader);

            // Hacking set readonly fields here, can't think of a cleaner workaround
            XmlExt.SetReadonlyField(ref this, x => x.X, XmlConvert.ToDouble(e.ReadAttributeOrElementOrDefault("X")));
            XmlExt.SetReadonlyField(ref this, x => x.Y, XmlConvert.ToDouble(e.ReadAttributeOrElementOrDefault("Y")));
            XmlExt.SetReadonlyField(ref this, x => x.Z, XmlConvert.ToDouble(e.ReadAttributeOrElementOrDefault("Z")));
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttribute("X", this.X);
            writer.WriteAttribute("Y", this.Y);
            writer.WriteAttribute("Z", this.Z);
        }

        public static UnitVector3DDto ReadFrom(XmlReader reader)
        {
            var v = new UnitVector3DDto();
            v.ReadXml(reader);
            return v;
        }


      public Vector3DDto ToVector3DDto() { throw new NotImplementedException(); }
      public static UnitVector3DDto Parse(string s) { throw new NotImplementedException(); }
    }
}
