using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using static GreedFlameTale.Enum.AttributeType;

namespace GreedFlameTale.Model.Character
{
    public class BaseCharacter : IGameCharacter
    {
        private protected Dictionary<HabilityType, IHability> _habilities;
        private protected Dictionary<AttributeType, IClampedUnit> _attributes;
        
        private string _name;

        public string Name => this._name;
        public bool IsAlive => !(this._attributes[HIT_POINTS].IsEmpty);

        private protected BaseCharacter(string name)
        {
            this._name = name;
        }

        public IClampedUnit GetAttribute(AttributeType name)
        {
            throw new System.NotImplementedException();
        }

        public IHability GetHability(HabilityType name)
        {
            throw new System.NotImplementedException();
        }
    }
}