using System;

namespace Spatial.Serialization.Xml.Euclidean
{
    [Serializable]
    public struct Circle3DDto
    {
        public readonly Point3DDto CenterPoint;
        public readonly UnitVector3DDto Axis;
        public readonly double Radius;

        public Circle3DDto(Point3DDto centerPoint, UnitVector3DDto axis, double radius)
        {
            this.CenterPoint = centerPoint;
            this.Axis = axis;
            this.Radius = radius;
        }
    }
}
