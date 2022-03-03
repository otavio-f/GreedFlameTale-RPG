using System;
using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using GreedFlameTale.Model.Hability;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Character
{
    public class Knight : BaseCharacter
    {
        private class SpecialAttack : SpecialAttackBase
        {
            public SpecialAttack(string name, Knight caster) : base(name, caster) { }

            private void Slash(IGameCharacter target, IClampedUnit damage)
            {
                var hp = target.GetAttribute(HIT_POINTS);
                var armor = target.GetAttribute(ARMOR_POINTS);
                var realDamage = damage.Sub(armor);
                hp.DecreaseBy(realDamage);
                var reaction = target.GetHability(GET_ATTACKED);
                reaction.Cast();
            }

            public override void Cast(params IGameCharacter[] targets)
            {
                base.ApplyCost();

                var atk = this._attacker.GetAttribute(ATTACK_POINTS);
                var damage = atk.Add(atk);
                Array.ForEach(targets, target => { Slash(target, damage); });
            }
        }

        private class OnAttacked: SpecialAttackBase
        {
            public OnAttacked(string name, Knight attacked) : base(name, attacked) { }

            public override void Cast(params IGameCharacter[] targets)
            {
                var stamina = this._attacker.GetAttribute(ATTACK_POINTS);
                stamina.IncreaseBy(new Measure(1));
            }
        }

        public Knight(string name) : base(name)
        {

            base._attributes = new Dictionary<AttributeType, IClampedUnit>()
            {
                { HIT_POINTS, new Measure(10) },
                { STAMINA, new Measure(10) },
                { MAGIC_POINTS, new Measure(0) },
                { ATTACK_POINTS, new Measure(3) },
                { ARMOR_POINTS, new Measure(2) },
                { NORMAL_STAMINA_COST, new Measure(2) },
                { SPECIAL_STAMINA_COST, new Measure(5) },
                { HEAL_CAPACITY, new Measure(1) },
                { REST_CAPACITY, new Measure(1) }
            };

            this._habilities = new Dictionary<HabilityType, IHability>()
            {
                { ATTACK, new Attack("Golpe de Espada", this) },
                { SPECIAL_ATTACK, new SpecialAttack("Corte Profundo", this) },
                { SELF_HEAL, new Heal("Bandagem", this) },
                { REST, new Rest("Meditação", this) },
                { GET_ATTACKED, new OnAttacked("Bravura", this) },
                { TURN, new Nothing() }
            };
        }
    }
}