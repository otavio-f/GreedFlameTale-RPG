using System;

namespace GreedFlameTale.Model
{
    public class Measure
    {
        public ushort Value { get; private set; }
        public ushort Maximum { get; init; }
        public bool IsFull => this.Value == this.Maximum;
        public bool IsEmpty => this.Value == 0;

        public Measure(ushort maximum)
        {
            this.Value = maximum;
            this.Maximum = maximum;
        }

        public Measure(long value, long maximum)
        {
            if (maximum > ushort.MaxValue || maximum < 0)
                throw new ArgumentException("Invalid maximum value.");
            this.Maximum = (ushort) maximum;
            this.Value = (ushort) Math.Clamp(value, 0, maximum);
        }

        public void Empty() => this.Value = 0;

        public void Fill() => this.Value = this.Maximum;

        public void DecreaseBy(Measure other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this.Value = (ushort) newVal;
        }

        public void IncreaseBy(Measure other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this.Value = (ushort) newVal;
        }

        public Measure Clone() => new Measure(this.Value, this.Maximum);

        public int CompareTo(Measure other) => this.Value.CompareTo(other.Value);

        public static Measure operator + (Measure a, Measure b)
        {
            var newMax = a.Maximum + b.Maximum;
            var newVal = a.Value + b.Value;
            return new Measure(newVal, newMax);
        }

        public static Measure operator - (Measure a, Measure b)
        {
            var newMax = Math.Max(a.Maximum, b.Maximum);
            var newVal = a.Value - b.Value;
            return new Measure(newVal, newMax);
        }
    }
}