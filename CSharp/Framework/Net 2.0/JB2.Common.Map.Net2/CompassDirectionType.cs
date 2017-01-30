using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface IAttribute<T>
    {
        T Value { get; }
    }


}

namespace JB2.Common.Enum
{
    public sealed class DegreeAttribute : Attribute, IAttribute<float>
    {
        private readonly float value;
        public DegreeAttribute(float value)
        {
            this.value = value;
        }
        public float Value
        {
            get { return this.value; }
        }
    }

    public enum CompassDirectionType
    {

        
        [Degree(0)]
        North = 1,

        NorthNorthEast = 2,

        [Degree(45)]
        NorthEast = 3,

        EastNorthEast = 4,

        [Degree(90)]
        East = 5,

        EastSouthEast = 6,


        [Degree(135)]
        SouthEast = 7,

        SouthSouthEast = 8,

        [Degree(180)]
        South = 9,

        SouthSouthWest = 10,

        [Degree(225)]
        SouthWest = 11,

        WestSouthWest = 12,

        [Degree(270)]
        West = 13,

        WestNorthWest = 14,

        [Degree(315)]
        NorthWest = 15,
        
        NorthNorthWest = 16

    }
}
