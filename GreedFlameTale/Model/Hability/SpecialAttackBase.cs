using GreedFlameTale.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedFlameTale.Model.Hability
{
    class SpecialAttackBase : IHability
    {
        private string _name;
        private protected IGameCharacter _attacker;

        public string Name => this._name;

        public SpecialAttackBase(string name, IGameCharacter attacker)
        {
            this._name = name;
            this._attacker = attacker;
        }

        private protected virtual void ApplyCost()
        {
            var stamina = this._attacker.GetAttribute(Enum.AttributeType.STAMINA);
            var cost = this._attacker.GetAttribute(Enum.AttributeType.SPECIAL_STAMINA_COST);
            stamina.DecreaseBy(cost);
        }

        public virtual bool CanCast()
        {
            var stamina = this._attacker.GetAttribute(Enum.AttributeType.STAMINA);
            var cost = this._attacker.GetAttribute(Enum.AttributeType.SPECIAL_STAMINA_COST);
            return stamina.CompareTo(cost) >= 0;
        }

        public virtual void Cast(params IGameCharacter[] targets)
        {
            throw new NotImplementedException();
        }
    }
}
