using System;
using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using GreedFlameTale.Model.Hability;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Character
{
    public class Duelist : BaseCharacter
    {
        private class SpecialAttack : SpecialAttackBase
        {
            public SpecialAttack(string name, Duelist caster) : base(name, caster) { }

            private void Slash(IGameCharacter target, IClampedUnit damage)
            {
                var hp = target.GetAttribute(HIT_POINTS);
                hp.DecreaseBy(damage);
                var reaction = target.GetHability(GET_ATTACKED);
                reaction.Cast();
            }

            public override void Cast(params IGameCharacter[] targets)
            {
                base.ApplyCost();
                var damage = this._attacker.GetAttribute(ATTACK_POINTS).Clone();
                Array.ForEach(targets, target => { Slash(target, damage); });
            }
        }

        public Duelist(string name) : base(name)
        {

            base._attributes = new Dictionary<AttributeType, IClampedUnit>()
            {
                { HIT_POINTS, new Measure(10) },
                { STAMINA, new Measure(10) },
                { MAGIC_POINTS, new Measure(0) },
                { ATTACK_POINTS, new Measure(3) },
                { ARMOR_POINTS, new Measure(1) },
                { NORMAL_STAMINA_COST, new Measure(2) },
                { SPECIAL_STAMINA_COST, new Measure(3) },
                { HEAL_CAPACITY, new Measure(1) },
                { REST_CAPACITY, new Measure(1) }
            };

            this._habilities = new Dictionary<HabilityType, IHability>()
            {
                { ATTACK, new Attack("Rapier", this) },
                { SPECIAL_ATTACK, new SpecialAttack("Perfuração", this) },
                { SELF_HEAL, new Heal("Bandagem", this) },
                { REST, new Rest("Meditação", this) },
                { GET_ATTACKED, new Nothing() },
                { TURN, new Nothing() }
            };
        }
    }
}