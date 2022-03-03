using System;

namespace GreedFlameTale.Interface
{
    public interface IClampedUnit : IComparable<IClampedUnit>
    {
        ushort Value {get;}
        ushort Maximum {get;}
        bool IsFull {get;}
        bool IsEmpty {get;}

        void Empty();
        void Fill();
        void DecreaseBy(IClampedUnit other);
        void IncreaseBy(IClampedUnit other);
        IClampedUnit Add(IClampedUnit other);
        IClampedUnit Sub(IClampedUnit other);
        IClampedUnit Clone();
        
    }
}