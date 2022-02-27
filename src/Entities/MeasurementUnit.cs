using System;
using GameInterfaces;

namespace Greed_Flame_Tale_RPG
{
    /// <summary> Represents a measurement unit with a fixed upper limit and lower limit
    /// at zero. Has a variable current value that is always clamped between the upper
    /// and lower limits. </summary>
    class MeasurementUnit : IClampedUnit
    {
        /// <summary> The actual value. </summary>
        private ushort _current;

        /// <inheritdoc/>
        public ushort Current 
        {
            get { return this._current; }
        }

        /// <summary> The actual upper limit. </summary>
        private ushort _maximum {get; set;}

        /// <inheritdoc/>
        public ushort Maximum {
            get { return _maximum; }
        }

        /// <inheritdoc/>
        public double Percent 
        {
            get { return (double) this.Current / this._maximum; }
        }

        /// <inheritdoc/>
        public bool IsEmpty
        {
            get { return this.Current == 0; }
        }
        
        /// <inheritdoc/>
        public bool IsFull
        {
            get { return this.Current == this._maximum; }
        }

        /// <summary> Create a new instance.
        /// <br/> All parameters will be clamped to a value between 0 and 
        /// <see cref="ushort.MaxValue"/>. </summary>
        /// <param name="maximum"> The maximum value. </param>
        /// <param name="current"> The current value. </param>
        public MeasurementUnit(long maximum, long current)
        {
            this._maximum = (ushort) Math.Clamp(maximum, 0, ushort.MaxValue);
            this._current = (ushort) Math.Clamp(current, 0, this._maximum);
        }
        
        /// <summary> Create a new instance. </summary>
        /// <param name="maximum"> The maximum and current value. </param>
        public MeasurementUnit(ushort maximum)
        {
            this._maximum = maximum;
            this._current = maximum;
        }

        /// <inheritdoc/>
        public void IncreaseValueBy(IClampedUnit other)
        {
            int result = (int) this.Current + other.Current;
            this._current = (ushort) Math.Clamp(result, 0, this._maximum);
        }

        /// <inheritdoc/>
        public void DecreaseValueBy(IClampedUnit other)
        {
            int result = (int) this.Current - other.Current;
            this._current = (ushort) Math.Clamp(result, 0, this._maximum);
        }

        /// <summary> Compares this instance to another <see cref="IClampedUnit"/> </summary>
        /// <param name="other"> The other instance to compare against this </param>
        /// <returns> -1 if the value in this instance is less than that on the other,
        /// <br/>0 if the values of both instances are equal,
        /// <br/>1 if the current value of this instance is greater than that
        /// the current value on the other instance </returns>
        /// <exception cref="ArgumentNullException"> When the other instance is null </exception> 
        public int CompareTo(IClampedUnit other)
        {
            if (other == null) throw new ArgumentNullException();
            return this.Current.CompareTo(other.Current);
        }
        
        /// <summary> Create a new instance from the sum of two <see cref="MeasurementUnit"/> </summary>
        /// <param name="a"> The first operand </param>
        /// <param name="b"> The second operand </param>
        /// <returns> A <see cref="MeasurementUnit"/> with the sum of 
        /// <see cref="MeasurementUnit.Current"/> and <see cref="MeasurementUnit.Maximum"/>
        /// </returns>
        public static MeasurementUnit operator +(MeasurementUnit a, MeasurementUnit b)
        {
            var maximum = (uint) a._maximum + b._maximum;
            var current = (uint) a._current + b._current;
            return new MeasurementUnit(maximum, current);
        }
        
        /// <summary> Create a new instance from the difference of two <see cref="MeasurementUnit"/>
        /// </summary>
        /// <param name="a"> The first operand </param>
        /// <param name="b"> The second operand </param>
        /// <returns> A <see cref="MeasurementUnit"/> with the difference between the two 
        /// <see cref="MeasurementUnit.Current"/> value 
        /// and the greater <see cref="MeasurementUnit.Maximum"/> of the operands.
        /// </returns>
        public static MeasurementUnit operator -(MeasurementUnit a, MeasurementUnit b)
        {
            var maximum = Math.Max(a._maximum, b._maximum);
            var current = (int) a._current - b._current;
            return new MeasurementUnit(maximum, current);
        }
    }
}
