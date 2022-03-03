using System;
using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using GreedFlameTale.Model.Hability;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Character
{
    public class Giant : BaseCharacter
    {
        private class SpecialAttack : SpecialAttackBase
        {
            public SpecialAttack(string name, Giant attacker): base(name, attacker) { }

            private void Smash(IGameCharacter target, IClampedUnit damage)
            {
                var hp = target.GetAttribute(HIT_POINTS);
                var armor = target.GetAttribute(ARMOR_POINTS);
                hp.DecreaseBy(damage.Sub(armor));
                var reaction = target.GetHability(GET_ATTACKED);
                reaction.Cast();
            }

            public override void Cast(params IGameCharacter[] targets)
            {
                base.ApplyCost();
                var atk = this._attacker.GetAttribute(ATTACK_POINTS);
                var damage = atk.Add(atk);
                Array.ForEach(targets, target => { this.Smash(target, damage); });
            }
        }

        private class OnAttacked : SpecialAttackBase
        {
            public OnAttacked(string name, Giant attacked): base(name, attacked) { }

            public override void Cast(params IGameCharacter[] targets)
            {
                var hp = this._attacker.GetAttribute(HIT_POINTS);
                hp.IncreaseBy(new Measure(1));
            }
        }

        public Giant(string name) : base(name)
        {
            base._attributes = new Dictionary<AttributeType, IClampedUnit>()
            {
                { HIT_POINTS, new Measure(20) },
                { STAMINA, new Measure(5) },
                { MAGIC_POINTS, new Measure(0) },
                { ATTACK_POINTS, new Measure(5) },
                { ARMOR_POINTS, new Measure(1) },
                { NORMAL_STAMINA_COST, new Measure(3) },
                { SPECIAL_STAMINA_COST, new Measure(5) },
                { HEAL_CAPACITY, new Measure(1) },
                { REST_CAPACITY, new Measure(1) }
            };

            this._habilities = new Dictionary<HabilityType, IHability>()
            {
                { ATTACK, new Attack("Pancada", this) },
                { SPECIAL_ATTACK, new SpecialAttack("Esmagar", this) },
                { SELF_HEAL, new Heal("Regenerar", this) },
                { REST, new Rest("Meditação", this) },
                { GET_ATTACKED, new OnAttacked("Ira do Gigante", this) },
                { TURN, new Nothing() }
            };
        }
    }
}