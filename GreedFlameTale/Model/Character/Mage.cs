using System;
using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using GreedFlameTale.Model.Hability;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Character
{
    public class Mage : BaseCharacter
    {
        private class SpecialAttack : SpecialAttackBase
        {
            public SpecialAttack(string name, Mage caster) : base(name, caster) { }

            private void HugeExplosion(IGameCharacter target, IClampedUnit damage)
            {
                var hp = target.GetAttribute(HIT_POINTS);
                hp.DecreaseBy(damage);
                var reaction = target.GetHability(GET_ATTACKED);
                reaction.Cast();
            }

            public override void Cast(params IGameCharacter[] targets)
            {
                base.ApplyCost();
                var magic = this._attacker.GetAttribute(MAGIC_POINTS);
                var damage = magic.Add(magic);
                Array.ForEach(targets, target => { HugeExplosion(target, damage); });
            }
        }

        public Mage(string name) : base(name)
        {
            base._attributes = new Dictionary<AttributeType, IClampedUnit>()
            {
                { HIT_POINTS, new Measure(10) },
                { STAMINA, new Measure(10) },
                { MAGIC_POINTS, new Measure(3) },
                { ATTACK_POINTS, new Measure(1) },
                { ARMOR_POINTS, new Measure(0) },
                { NORMAL_STAMINA_COST, new Measure(3) },
                { SPECIAL_STAMINA_COST, new Measure(6) },
                { HEAL_CAPACITY, new Measure(1) },
                { REST_CAPACITY, new Measure(2) }
            };

            this._habilities = new Dictionary<HabilityType, IHability>()
            {
                { ATTACK, new Attack("Lançar Projétil", this) },
                { SPECIAL_ATTACK, new SpecialAttack("Explosão", this) },
                { SELF_HEAL, new Heal("Cura Concentrada", this) },
                { REST, new Rest("Meditação", this) },
                { GET_ATTACKED, new Nothing() },
                { TURN, new Nothing() }
            };
        }
    }
}