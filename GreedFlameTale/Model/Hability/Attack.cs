using System;
using GreedFlameTale.Interface;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Hability
{
    public class Attack : IHability
    {
        private string _name;
        private IGameCharacter _attacker;

        public string Name => this._name;

        public Attack(string name, IGameCharacter attacker)
        {
            _name = name;
            _attacker = attacker;
        }

        public bool CanCast()
        {
            var cost = this._attacker.GetAttribute(NORMAL_STAMINA_COST);
            var stamina = this._attacker.GetAttribute(STAMINA);
            return stamina.CompareTo(cost) >= 0;
        }

        private void ApplyCost()
        {
            var cost = this._attacker.GetAttribute(NORMAL_STAMINA_COST);
            var stamina = this._attacker.GetAttribute(STAMINA);
            stamina.DecreaseBy(cost);
        }

        private IClampedUnit GetRawDamage()
        {
            var attack = this._attacker.GetAttribute(ATTACK_POINTS);
            var magic = this._attacker.GetAttribute(MAGIC_POINTS);
            return attack.Add(magic);
        }

        private void AttackOne(IGameCharacter target, IClampedUnit rawDamage)
        {
            var armor = target.GetAttribute(ARMOR_POINTS);
            var damage = rawDamage.Sub(armor);
            var hp = target.GetAttribute(HIT_POINTS);
            hp.DecreaseBy(damage);
            var reaction = target.GetHability(GET_ATTACKED); //CAST THIS TO THE CORRECT TYPE!
            reaction.Cast(this._attacker);
        }

        public void Cast(params IGameCharacter[] targets)
        {
            ApplyCost();

            var rawDamage = GetRawDamage();

            Array.ForEach(targets, target => { AttackOne(target, rawDamage); });
        }
    }
}