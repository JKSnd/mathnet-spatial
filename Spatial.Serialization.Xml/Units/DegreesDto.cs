using System;
using System.ComponentModel;
using MathNet.Spatial.Units;

namespace Spatial.Serialization.Xml.Units
{
  [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
    public struct DegreesDto : IAngleUnit
    {
        private const double Conv = Math.PI / 180.0;
        internal const string Name = "\u00B0";

        public double Conversionfactor
        {
            get
            {
                return Conv;
            }
        }

        public string ShortName
        {
            get
            {
                return Name;
            }
        }

        public static AngleDto operator *(double left, DegreesDto right)
        {
            return new AngleDto(left, right);
        }
    }
}