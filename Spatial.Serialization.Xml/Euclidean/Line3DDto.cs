using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using MathNet.Numerics;
using Spatial.Serialization.Xml.Units;

namespace Spatial.Serialization.Xml.Euclidean
{
    /// <summary>
    /// A line between two points
    /// </summary>
    [Serializable]
    public struct Line3DDto : IEquatable<Line3DDto>, IXmlSerializable
    {
        /// <summary>
        /// The startpoint of the line
        /// </summary>
        public readonly Point3DDto StartPoint;

        /// <summary>
        /// The endpoint of the line
        /// </summary>
        public readonly Point3DDto EndPoint;
        private double _length;
        private UnitVector3DDto _direction;

        /// <summary>
        /// Throws if StartPoint == EndPoint
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Line3DDto(Point3DDto startPoint, Point3DDto endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            if (this.StartPoint.Equals(this.EndPoint))
            {
                throw new ArgumentException("StartPoint == EndPoint");
            }

            this._length = -1.0;
            this._direction = new UnitVector3DDto();
        }

    

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Line3DDto other)
        {
            return this.StartPoint.Equals(other.StartPoint) && this.EndPoint.Equals(other.EndPoint);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Line3DDto && this.Equals((Line3DDto)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.StartPoint.GetHashCode();
                hashCode = (hashCode * 397) ^ this.EndPoint.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("StartPoint: {0}, EndPoint: {1}", this.StartPoint, this.EndPoint);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }
        
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var e = (XElement)XNode.ReadFrom(reader);
            var startPoint = Point3DDto.ReadFrom(e.SingleElement("StartPoint").CreateReader());
            XmlExt.SetReadonlyField(ref this, l => l.StartPoint, startPoint);
            var endPoint = Point3DDto.ReadFrom(e.SingleElement("EndPoint").CreateReader());
            XmlExt.SetReadonlyField(ref this, l => l.EndPoint, endPoint);
        }
        
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement("StartPoint", this.StartPoint);
            writer.WriteElement("EndPoint", this.EndPoint);
        }
    }
}
