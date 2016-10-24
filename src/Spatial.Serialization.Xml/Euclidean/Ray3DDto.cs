using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Spatial.Serialization.Xml.Euclidean
{
    [Serializable]
    public struct Ray3DDto : IEquatable<Ray3DDto>, IXmlSerializable, IFormattable
    {
        public readonly Point3DDto ThroughPoint;

        public readonly UnitVector3DDto Direction;

        public Ray3DDto(Point3DDto throughPoint, UnitVector3DDto direction)
        {
            this.ThroughPoint = throughPoint;
            this.Direction = direction;
        }

    
        public bool Equals(Ray3DDto other)
        {
            return this.Direction.Equals(other.Direction) &&
                   this.ThroughPoint.Equals(other.ThroughPoint);
        }

        public bool Equals(Ray3DDto other, double tolerance)
        {
            return this.Direction.Equals(other.Direction, tolerance) &&
                   this.ThroughPoint.Equals(other.ThroughPoint, tolerance);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Ray3DDto && this.Equals((Ray3DDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.ThroughPoint.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Direction.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.ToString(null, CultureInfo.InvariantCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(
                "ThroughPoint: {0}, Direction: {1}",
                this.ThroughPoint.ToString(format, formatProvider),
                this.Direction.ToString(format, formatProvider));
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var e = (XElement)XNode.ReadFrom(reader);
            XmlExt.SetReadonlyField(ref this, l => l.ThroughPoint, Point3DDto.ReadFrom(e.SingleElement("ThroughPoint").CreateReader()));
            XmlExt.SetReadonlyField(ref this, l => l.Direction, UnitVector3DDto.ReadFrom(e.SingleElement("Direction").CreateReader()));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement("ThroughPoint", this.ThroughPoint);
            writer.WriteElement("Direction", this.Direction);
        }
    }
}