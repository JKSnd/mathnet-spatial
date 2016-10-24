using System;
using System.ComponentModel;
using MathNet.Spatial.Units;

namespace Spatial.Serialization.Xml.Units
{
  [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
    public struct RadiansDto : IAngleUnit
    {
        private const double Conv = 1.0;
        internal const string Name = "rad";

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

        public static AngleDto operator *(double left, RadiansDto right)
        {
            return new AngleDto(left, right);
        }
    }
}