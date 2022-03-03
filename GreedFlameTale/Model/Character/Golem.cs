using System;
using System.Collections.Generic;
using GreedFlameTale.Enum;
using GreedFlameTale.Interface;
using GreedFlameTale.Model.Hability;
using static GreedFlameTale.Enum.AttributeType;
using static GreedFlameTale.Enum.HabilityType;

namespace GreedFlameTale.Model.Character
{
    public class Golem : BaseCharacter
    {

        private class SpecialAttack : SpecialAttackBase
        {
            private SpecialAttackBase _onattack;
            public SpecialAttack(string name, Golem attacker): base(name, attacker)
            {
                this._onattack = new OnAttack("Erosão", attacker);
            }

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
                var hp = this._attacker.GetAttribute(HIT_POINTS);
                hp.DecreaseBy(new Measure(1));
                var atk = this._attacker.GetAttribute(ATTACK_POINTS);
                var damage = atk.Add(atk);
                Array.ForEach(targets, target => { this.Smash(target, damage); });
                this._onattack.Cast();
            }
        }

        private class OnAttack : SpecialAttackBase
        {
            public OnAttack(string name, Golem active) : base(name, active) { }

            public override void Cast(params IGameCharacter[] targets)
            {
                var hp = this._attacker.GetAttribute(HIT_POINTS);
                hp.DecreaseBy(new Measure(1));
            }
        }

        private class OnTurn : SpecialAttackBase
        {
            public OnTurn(string name, Golem active): base(name, active) { }

            public override void Cast(params IGameCharacter[] targets)
            {
                var hp = this._attacker.GetAttribute(HIT_POINTS);
                hp.IncreaseBy(new Measure(1));
            }
        }

        public Golem(string name) : base(name)
        {
            base._attributes = new Dictionary<AttributeType, IClampedUnit>()
            {
                { HIT_POINTS, new Measure(30) },
                { STAMINA, new Measure(2) },
                { MAGIC_POINTS, new Measure(0) },
                { ATTACK_POINTS, new Measure(5) },
                { ARMOR_POINTS, new Measure(0) },
                { NORMAL_STAMINA_COST, new Measure(1) },
                { SPECIAL_STAMINA_COST, new Measure(2) },
                { HEAL_CAPACITY, new Measure(0) },
                { REST_CAPACITY, new Measure(0) }
            };

            this._habilities = new Dictionary<HabilityType, IHability>()
            {
                { ATTACK, new Attack("Pancada", this) },
                { SPECIAL_ATTACK, new SpecialAttack("Esmagar", this) },
                { SELF_HEAL, new Heal("Reconstrução", this) },
                { REST, new Rest("Meditação", this) },
                { GET_ATTACKED, new Nothing() },
                { TURN, new OnTurn("Maldição do Aprisionamento", this) }
            };
        }
    }
}