using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using MathNet.Spatial.Units;

namespace Spatial.Serialization.Xml.Units
{
  /// <summary>
    /// An AngleDto
    /// </summary>
    [Serializable]
    public struct AngleDto : IComparable<AngleDto>, IEquatable<AngleDto>, IFormattable, IXmlSerializable
    {
        /// <summary>
        /// The value in radians
        /// </summary>
        public readonly double Radians;

        private AngleDto(double radians)
        {
            this.Radians = radians;
        }

        /// <summary>
        /// Initializes a new instance of the AngleDto.
        /// </summary>
        /// <param name="radians"></param>
        /// <param name="unit"></param>
        public AngleDto(double radians, RadiansDto unit)
        {
            this.Radians = radians;
        }

        /// <summary>
        /// Initializes a new instance of the AngleDto.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public AngleDto(double value, DegreesDto unit)
        {
            this.Radians = UnitConverter.ConvertFrom(value, unit);
        }



        /// <summary>
        /// Creates a new instance of AngleDto.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public static AngleDto From<T>(double value, T unit) where T : IAngleUnit
        {
            return new AngleDto(UnitConverter.ConvertFrom(value, unit));
        }

        public override string ToString()
        {
            return this.ToString((string)null, (IFormatProvider)NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return this.ToString(format, (IFormatProvider)NumberFormatInfo.CurrentInfo);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString((string)null, (IFormatProvider)NumberFormatInfo.GetInstance(provider));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString(format, formatProvider, AngleUnit.Radians);
        }

        public string ToString<T>(string format, IFormatProvider formatProvider, T unit) where T : IAngleUnit
        {
            var value = UnitConverter.ConvertTo(this.Radians, unit);
            return string.Format("{0}{1}", value.ToString(format, formatProvider), unit.ShortName);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="T:MathNet.Spatial.Units.AngleDto"/> object and returns an integer that indicates whether this <see cref="instance"/> is shorter than, equal to, or longer than the <see cref="T:MathNet.Spatial.Units.AngleDto"/> object.
        /// </summary>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="value"/>.
        /// 
        ///                     Value
        /// 
        ///                     Description
        /// 
        ///                     A negative integer
        /// 
        ///                     This instance is smaller than <paramref name="value"/>.
        /// 
        ///                     Zero
        /// 
        ///                     This instance is equal to <paramref name="value"/>.
        /// 
        ///                     A positive integer
        /// 
        ///                     This instance is larger than <paramref name="value"/>.
        /// 
        /// </returns>
        /// <param name="value">A <see cref="T:MathNet.Spatial.Units.AngleDto"/> object to compare to this instance.</param>
        public int CompareTo(AngleDto value)
        {
            return this.Radians.CompareTo(value.Radians);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/> object.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same AngleDto as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/> object to compare with this instance.</param>
        public bool Equals(AngleDto other)
        {
            return this.Radians.Equals(other.Radians);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same AngleDto as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(AngleDto other, double tolerance)
        {
            return Math.Abs(this.Radians - other.Radians) < tolerance;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is AngleDto && this.Equals((AngleDto)obj);
        }

        public override int GetHashCode()
        {
            return this.Radians.GetHashCode();
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, 
        /// you should return null (Nothing in Visual Basic) from this method, and instead, 
        /// if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the
        ///  <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> 
        /// method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
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
            XmlExt.SetReadonlyField(ref this, x => x.Radians, XmlConvert.ToDouble(e.ReadAttributeOrElementOrDefault("Value")));
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttribute("Value", this.Radians);
        }

        /// <summary>
        /// Reads an instance of <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/> from the <paramref name="reader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>An instance of  <see cref="T:Spatial.Serialization.Xml.Units.AngleDto"/></returns>
        public static AngleDto ReadFrom(XmlReader reader)
        {
            var v = new AngleDto();
            v.ReadXml(reader);
            return v;
        }
    }
}