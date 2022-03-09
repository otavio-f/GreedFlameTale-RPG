using System;
using System.Collections.Generic;

namespace GreedFlameTale.Model
{
    /// <summary>
    /// Class that represents a measurement unit with a reference and current values.
    /// </summary>
    public class Measure : IEquatable<Measure>
    {
        /// <summary>
        /// The current value.
        /// Is always clamped between <see langword="0"/> and <see cref="Value"/>
        /// </summary>
        public int Value {
            get { return Value; }
            private set { Math.Clamp(value, 0, this.Maximum); }
        }

        /// <summary>
        /// The upper limit.
        /// Is always clamped between <see langword="0"/> and <see cref="int.MaxValue"/>
        /// </summary>
        public int Maximum {
            get { return Maximum; }
            private set { Math.Clamp(value, 0, int.MaxValue); }
        }

        /// <summary>
        /// <see langword="true"/> if the <see cref="Value"/> is equal to the <see cref="Maximum"/>,
        /// otherwise <see langword="false"/>
        /// </summary>
        public bool IsFull => this.Value == this.Maximum;

        /// <summary>
        /// <see langword="true"/> if the <see cref="Measure.Value"/> is <see langword="0"/>,
        /// otherwise <see langword="false"/>
        /// </summary>
        public bool IsEmpty => this.Value == 0;

        /// <summary>
        /// The percentage of <see cref="Value"/> relative to <see cref="Maximum"/>.
        /// It is always 0 on negative <see cref="Value"/>
        /// </summary>
        public ushort Percentage
        {
            get
            {
                if (this.Maximum > 0 && this.Value > 0) 
                    return (ushort) Math.Ceiling(100.0 * this.Value / this.Maximum);
                return 0;
            }
        }

        /// <summary>
        /// Constructor to set both the upper limit and value as positive 16-bit integers
        /// </summary>
        /// <param name="maximum">Sets both the <see cref="Maximum"/> and <see cref="Value"/></param>
        public Measure(ushort maximum)
        {
            this.Value = maximum;
            this.Maximum = maximum;
        }

        /// <summary>
        /// Constructor that sets the upper limit and value separately
        /// </summary>
        /// <param name="value">The <see cref="Value"/></param>
        /// <param name="maximum">The <see cref="Maximum"/> </param>
        public Measure(int value, int maximum)
        {
            this.Value = value;
            this.Maximum = maximum;
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
        public void DecreaseBy(Measure other) => this.Value -= other.Value;

        /// <summary>
        /// Increases the <see cref="Value"/> in this instance from the <see cref="Value"/> in other <see cref="Measure"/> instance
        /// </summary>
        /// <param name="other"></param>
        public void IncreaseBy(Measure other) => this.Value += other.Value;

        /// <summary>
        /// Offset both the upper limit and value
        /// </summary>
        /// <param name="value">The offset.</param>
        public void OffsetBy(int offset)
        {
            this.Maximum += offset;
            this.Value += offset;
        }

        /// <summary>
        /// Gets a shallow clone of this instance
        /// </summary>
        /// <returns>A <see cref="Measure"/> with the same value and upper limit</returns>
        public Measure Clone() => new Measure(this.Value, (ushort) this.Maximum);

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

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as Measure);

        /// <inheritdoc/>
        public bool Equals(Measure other)
        {
            return other != null &&
                   Value == other.Value &&
                   Maximum == other.Maximum;
        }

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Value, Maximum);

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
        public static Measure operator -(Measure a, Measure b)
        {
            var newMax = Math.Max(a.Maximum, b.Maximum);
            var newVal = a.Value - b.Value;
            return new Measure(newVal, newMax);
        }

        /// <inheritdoc/>
        public static bool operator ==(Measure left, Measure right)
        {
            return EqualityComparer<Measure>.Default.Equals(left, right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Measure left, Measure right)
        {
            return !(left == right);
        }
    }
}