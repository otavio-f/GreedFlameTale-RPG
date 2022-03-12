using System;

namespace GreedFlameTale.Model.Attribute
{
    /// <summary>
    /// Class that represents a measurement unit with a reference and current values.
    /// </summary>
    public class CharacterAttribute
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
        /// <see langword="true"/> if the <see cref="CharacterAttribute.Value"/> is <see langword="0"/>,
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
        public CharacterAttribute(ushort maximum)
        {
            this.Value = maximum;
            this.Maximum = maximum;
        }

        /// <summary>
        /// Constructor that sets the upper limit and value separately
        /// </summary>
        /// <param name="value">The <see cref="Value"/></param>
        /// <param name="maximum">The <see cref="Maximum"/> </param>
        public CharacterAttribute(int value, int maximum)
        {
            this.Value = value;
            this.Maximum = maximum;
        }

        /// <summary>
        /// Decreases the <see cref="Value"/> in this instance from the <see cref="Value"/> in other <see cref="CharacterAttribute"/> instance
        /// </summary>
        /// <param name="other"></param>
        public void DecreaseBy(CharacterAttribute other) => this.Value -= other.Value;

        /// <summary>
        /// Increases the <see cref="Value"/> in this instance from the <see cref="Value"/> in other <see cref="CharacterAttribute"/> instance
        /// </summary>
        /// <param name="other"></param>
        public void IncreaseBy(CharacterAttribute other) => this.Value += other.Value;

        /// <summary>
        /// Offset both the upper limit and value
        /// </summary>
        /// <param name="value">The offset.</param>
        public void OffsetBy(int value)
        {
            this.Maximum += value;
            this.Value += value;
        }

        /// <summary>
        /// Gets a shallow clone of this instance
        /// </summary>
        /// <returns>A <see cref="CharacterAttribute"/> with the same value and upper limit</returns>
        public CharacterAttribute Clone() => new CharacterAttribute(this.Value, (ushort) this.Maximum);

        /// <summary>
        /// Compares the <see cref="Value"/> between this instance and another <see cref="CharacterAttribute"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        /// <see langword="1"/> if the <see cref="Value"/> of this instance is greater<br/>
        /// <see langword="0"/> if the <see cref="Value"/> of this instance is equal to the other<br/>
        /// <see langword="-1"/> if the <see cref="Value"/> of this unit is lower
        /// </returns>
        public int CompareTo(CharacterAttribute other) => this.Value.CompareTo(other.Value);

        /// <summary>
        /// Sums the <see cref="Value"/> and <see cref="Maximum"/> from two instances of <see cref="CharacterAttribute"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The resulting <see cref="CharacterAttribute"/></returns>
        public static CharacterAttribute operator + (CharacterAttribute a, CharacterAttribute b)
        {
            var newMax = a.Maximum + b.Maximum;
            var newVal = a.Value + b.Value;
            return new CharacterAttribute(newVal, newMax);
        }

        /// <summary>
        /// Gets the difference between the <see cref="Value"/> of two <see cref="CharacterAttribute"/>
        /// keeping the higher <see langword="Maximum"/> of the two.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The resulting <see cref="CharacterAttribute"/></returns>
        public static CharacterAttribute operator -(CharacterAttribute a, CharacterAttribute b)
        {
            var newMax = Math.Max(a.Maximum, b.Maximum);
            var newVal = a.Value - b.Value;
            return new CharacterAttribute(newVal, newMax);
        }

    }
}