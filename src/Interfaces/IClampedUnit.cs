using System;
namespace GameInterfaces
{
    /// <summary>Represents a measurement unit with a fixed upper limit and
    /// a variable value in the range 0 to <see cref="IClampedUnit.Maximum"/>
    /// </summary>
    public interface IClampedUnit : IComparable<IClampedUnit>
    {
        /// <summary>The variable value.</summary>
        ushort Current {get;}

        /// <summary>The fixed upper limit.</summary>
        ushort Maximum {get;}

        /// <summary>The percentage relative to the upper limit.</summary>
        double Percent {get;}

        /// <summary>True if the value is equal to 0.</summary>
        bool IsEmpty {get;}

        /// <summary>True if the value is equal to the upper limit.</summary>
        bool IsFull {get;}

        /// <summary>Adds the value of another instance of <see cref="IClampedUnit.Current"/>
        /// to this instance.</summary>
        /// <param name="o">The other <see cref="IClampedUnit"/></param>
        public void IncreaseValueBy(IClampedUnit o);

        /// <summary>Subtracts the value of another instance of <see cref="IClampedUnit.Current"/>
        /// from the value of this instance.</summary>
        /// <param name="o">The other <see cref="IClampedUnit"/></param>
        public void DecreaseValueBy(IClampedUnit o);
        
    }
}