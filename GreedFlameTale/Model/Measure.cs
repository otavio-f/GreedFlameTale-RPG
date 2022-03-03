using System;
using GreedFlameTale.Interface;

namespace GreedFlameTale.Model
{
    public class Measure : IClampedUnit
    {

        private ushort _value;
        private ushort _maximum;

        public ushort Value => this._value;
        public ushort Maximum => this._maximum;
        bool IClampedUnit.IsFull => this._value == this._maximum;
        bool IClampedUnit.IsEmpty => this._value == 0;

        public Measure(ushort maximum)
        {
            this._value = maximum;
            this._maximum = maximum;
        }

        public Measure(long value, long maximum)
        {
            if (maximum > ushort.MaxValue || maximum < 0)
                throw new ArgumentException("Invalid maximum value.");
            this._maximum = (ushort) maximum;
            this._value = (ushort) Math.Clamp(value, 0, maximum);
        }

        public void Empty() => this._value = 0;

        public void Fill() => this._value = this._maximum;

        public IClampedUnit Add(IClampedUnit other)
        {
            var newMax = other.Maximum + this.Maximum;
            var newVal = other.Value + this.Value;
            return new Measure(newVal, newMax);
        }

        public IClampedUnit Sub(IClampedUnit other)
        {
            var newMax = Math.Max(other.Maximum, this.Maximum);
            var newVal = this.Value - other.Value;
            return new Measure(newVal, newMax);
        }

        public void DecreaseBy(IClampedUnit other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this._value = (ushort) newVal;
        }

        public void IncreaseBy(IClampedUnit other)
        {
            var newVal = Math.Max(0, this.Value - other.Value);
            this._value = (ushort) newVal;
        }

        public IClampedUnit Clone() => new Measure(this.Value, this.Maximum);

        public int CompareTo(IClampedUnit other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}