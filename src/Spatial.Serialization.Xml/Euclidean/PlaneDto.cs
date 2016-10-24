using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Spatial.Serialization.Xml.Euclidean
{
    [Serializable]
    public struct PlaneDto : IEquatable<PlaneDto>, IXmlSerializable
    {
        public readonly UnitVector3DDto Normal;
        public readonly Point3DDto RootPoint;

      

     

        public PlaneDto(Point3DDto rootPoint, UnitVector3DDto normal)
        {
            this.RootPoint = rootPoint;
            this.Normal = normal;
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

            return obj is PlaneDto && this.Equals((PlaneDto)obj);
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
                int result = this.Normal.GetHashCode();
                result = (result*397) ^ this.RootPoint.GetHashCode();
                return result;
            }
        }


      public bool Equals(PlaneDto other) { throw new NotImplementedException(); }

    
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var e = (XElement)XNode.ReadFrom(reader);
            XmlExt.SetReadonlyField(ref this, l => l.RootPoint, Point3DDto.ReadFrom(e.SingleElement("RootPoint").CreateReader()));
            XmlExt.SetReadonlyField(ref this, l => l.Normal, UnitVector3DDto.ReadFrom(e.SingleElement("Normal").CreateReader()));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElement("RootPoint", this.RootPoint);
            writer.WriteElement("Normal", this.Normal);
        }


      public static PlaneDto Parse(string p) { throw new NotImplementedException(); }
    }
}
