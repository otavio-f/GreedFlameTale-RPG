using System;

namespace GreedFlameTale.Model
{
    /// <summary>
    /// Class that represents a measurement unit with a value between an upper and lower limit.
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// The current value, range from <see langword="0"/> to <see cref="Measure.Maximum"/>.
        /// </summary>
        public ushort Value { get; private set; }

        /// <summary>
        /// The upper limit.
        /// </summary>
        public ushort Maximum { get; private set; }

        /// <summary>
        /// <see langword="true"/> if the <see cref="Measure.Value"/> is equal to the <see cref="Measure.Maximum"/>,
        /// otherwise <see langword="false"/>
        /// </summary>
        public bool IsFull => this.Value == this.Maximum;

        /// <summary>
        /// <see langword="true"/> if the <see cref="Measure.Value"/> is <see langword="0"/>,
        /// otherwise <see langword="false"/>
        /// </summary>
        public bool IsEmpty => this.Value == 0;

        /// <summary>
        /// The percentage of "fullness", from 0 to 100
        /// </summary>
        public ushort Percentage => (ushort) Math.Ceiling(100.0 * this.Value / this.Maximum);

        
        /// <summary>
        /// Constructor to set both the upper limit and value
        /// </summary>
        /// <param name="maximum">Sets both the <see cref="Maximum"/> and <see cref="Value"/></param>
        public Measure(ushort maximum)
        {
            this.Value = maximum;
            this.Maximum = maximum;
        }

        /// <summary>
        /// Constructor to clamp and set the upper limit and value separately
        /// </summary>
        /// <param name="value">The <see cref="Value"/>, clamped</param>
        /// <param name="maximum">The <see cref="Maximum"/>, clamped between <see langword="0"/> and <see langword="ushort.MaxValue"/></param>
        public Measure(long value, long maximum)
        {
            if (maximum > ushort.MaxValue || maximum < 0)
                throw new ArgumentException("Invalid maximum value.");
            this.Maximum = (ushort) maximum;
            this.Value = (ushort) Math.Clamp(value, 0, maximum);
        }

        /// <summary>
        /// Sets the <see cref="Value"/> to <see langword="0"/>
        /// </summary>
        public void Empty() => this.Value = 0;

        /// <summary>
        /// Sets the <see cref="Value"/> to <see cref="Maximum"/>
        /// </summary>
        public void Fill() => this.Value = this.Maximum;

        /// <summary>
        /// Decreases the <see cref="Value"/> in this instance from the <see cref="Value"/> in other <see cref="Measure"/> instance
        /// </summary>
        /// <param name="other"></param>
        public void DecreaseBy(Measure other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this.Value = (ushort) newVal;
        }

        /// <summary>
        /// Increases the <see cref="Value"/> in this instance from the <see cref="Value"/> in other <see cref="Measure"/> instance
        /// </summary>
        /// <param name="other"></param>
        public void IncreaseBy(Measure other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this.Value = (ushort) newVal;
        }

        /// <summary>
        /// Offset both the upper limit and value
        /// </summary>
        /// <param name="value">The offset.</param>
        public void OffsetBy(long value)
        {
            this.Maximum = (ushort) Math.Clamp(this.Maximum + value, 0, ushort.MaxValue);
            this.Value = (ushort) Math.Clamp(this.Value + value, 0, this.Maximum);
        }

        /// <summary>
        /// Gets a shallow clone of this instance
        /// </summary>
        /// <returns>A <see cref="Measure"/> with the same value and upper limit</returns>
        public Measure Clone() => new Measure(this.Value, this.Maximum);

        /// <summary>
        /// Compares the <see cref="Value"/> between this instance and another <see cref="Measure"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// <see langword="1"/> if the <see cref="Value"/> of this instance is greater<br/>
        /// <see langword="0"/> if the <see cref="Value"/> of this instance is equal to the other<br/>
        /// <see langword="-1"/> if the <see cref="Value"/> of this unit is lower
        /// </returns>
        public int CompareTo(Measure other) => this.Value.CompareTo(other.Value);

        /// <summary>
        /// Sums the <see cref="Value"/> and <see cref="Maximum"/> from two instances of <see cref="Measure"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The resulting <see cref="Measure"/></returns>
        public static Measure operator + (Measure a, Measure b)
        {
            var newMax = a.Maximum + b.Maximum;
            var newVal = a.Value + b.Value;
            return new Measure(newVal, newMax);
        }

        /// <summary>
        /// Gets the difference between the <see cref="Value"/> of two <see cref="Measure"/>
        /// keeping the higher <see langword="Maximum"/> of the two.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The resulting <see cref="Measure"/></returns>
        public static Measure operator - (Measure a, Measure b)
        {
            var newMax = Math.Max(a.Maximum, b.Maximum);
            var newVal = a.Value - b.Value;
            return new Measure(newVal, newMax);
        }
    }
}