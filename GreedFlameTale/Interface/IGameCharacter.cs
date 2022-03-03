using System.Collections.Generic;
using GreedFlameTale.Enum;

namespace GreedFlameTale.Interface
{
    public interface IGameCharacter
    {
        string Name {get;}
        bool IsAlive {get;}
        IHability GetHability(HabilityType name);
        IClampedUnit GetAttribute(AttributeType name);
    }
}