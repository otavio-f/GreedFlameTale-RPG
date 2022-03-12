using GreedFlameTale.Model.Attribute;
using System;
using System.Collections.Generic;

namespace GreedFlameTale.Model.Hability
{
    public class HabilityBuilder
    {
        private string _name;
        private string _description;
        private CharacterAttribute _cost = new(0);
        private Character _caster;
        private List<Action<Character>> _actions = new();

        /// <summary>
        /// Creates a new HabilityBuilder with a caster
        /// </summary>
        /// <param name="caster">The <see cref="Character"/> using this hability.</param>
        public HabilityBuilder(Character caster)
        {
            _caster = caster;
        }

        /// <summary>
        /// Sets the name of this hability
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns></returns>
        public HabilityBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets the description of this hability.
        /// </summary>
        /// <param name="description">The description text</param>
        /// <returns></returns>
        public HabilityBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// Sets the hability cost in stamina used and adds a cost deduct action
        /// </summary>
        /// <param name="cost">The stamina used to cast this hability</param>
        /// <returns>This instance with a cost deduction action incorporated</returns>
        public HabilityBuilder WithCost(ushort cost)
        {
            _cost = new(cost);
            _actions.Add(_ =>
            {
                var sta = _caster.Attributes.Stamina;
                sta.DecreaseBy(_cost);
            });
            return this;
        }

        /// <summary>
        /// Adds a <see cref="AttributeHolder.Stamina"/> exhaustion action in this hability's routines
        /// to be used against a target, reducing it's stamina value.
        /// </summary>
        /// <param name="value">The <see cref="AttributeHolder.Stamina"/> reduction</param>
        /// <returns>The instance with an exhaustion action incorporated.</returns>
        public HabilityBuilder AddExhaustion(ushort value)
        {
            _actions.Add(target =>
            {
                var sta = target.Attributes.Stamina;
                sta.DecreaseBy(new(value));
            });
            return this;
        }

        /// <summary>
        /// Adds a default attack in this hability routines.<br/>
        /// The attack does not ignore the target's armor attribute
        /// and uses the caster's attack power attribute as damage.
        /// </summary>
        /// <returns>The instance with an attack incorporated.</returns>
        public HabilityBuilder AddAttack()
        {
            _actions.Add(target =>
            {
                var damage = _caster.Attributes.AttackPower - target.Attributes.Armor;
                var hp = target.Attributes.HitPoints;
                hp.DecreaseBy(damage);
            });
            return this;
        }

        /// <summary>
        /// Adds a default piercing attack in this hability routines.<br/>
        /// The attack ignore the target's armor attribute
        /// and uses the caster's attack power attribute as damage.
        /// </summary>
        /// <returns>The instance with an attack incorporated.</returns>
        public HabilityBuilder AddPiercingAttack()
        {
            _actions.Add(target =>
            {
                var damage = _caster.Attributes.AttackPower;
                var hp = target.Attributes.HitPoints;
                hp.DecreaseBy(damage);
            });
            return this;
        }

        /// <summary>
        /// Adds a custom attack into this hability routines.<br/>
        /// The attack can ignore target armor, and the power is composed of the sum of many <see cref="CharacterAttribute"/> values
        /// </summary>
        /// <param name="ignoreArmor">Indicates if the attack will ignore the target armor.</param>
        /// <param name="measures">The attributes used to determine attack power</param>
        /// <returns>The instance with an attack incorporated.</returns>
        public HabilityBuilder AddAttack(bool ignoreArmor, params CharacterAttribute[] measures)
        {
            var atkPower = new CharacterAttribute(0);
            Array.ForEach(measures, inc => { atkPower += inc; });
            _actions.Add(target =>
            {
                var damage = atkPower - target.Attributes.Armor;
                var hp = target.Attributes.HitPoints;
                hp.DecreaseBy(damage);
            });
            return this;
        }

        /// <summary>
        /// Adds a rest into this hability's routines<br/>
        /// It is used for recovering <see cref="AttributeHolder.HitPoints"/> and <see cref="AttributeHolder.Stamina"/>
        /// </summary>
        /// <returns>The instance with an rest incorporated.</returns>
        public HabilityBuilder AddResting()
        {
            _actions.Add(_ =>
            {
                var hp = _caster.Attributes.HitPoints;
                var heal = _caster.Attributes.HealPoints;
                hp.IncreaseBy(heal);
                var sta = _caster.Attributes.Stamina;
                var recover = _caster.Attributes.RestPoints;
                sta.IncreaseBy(recover);
            });
            return this;
        }

        private CharacterAttribute GetAttributeFrom(AttributeHolder holder, ModAttributeType attr)
        {
            CharacterAttribute result;
            switch (attr)
            {
                case ModAttributeType.HP:
                    result = holder.HitPoints;
                    break;
                case ModAttributeType.STA:
                    result = holder.Stamina;
                    break;
                case ModAttributeType.ATK:
                    result = holder.AttackPower;
                    break;
                case ModAttributeType.MP:
                    result = holder.MagicPower;
                    break;
                case ModAttributeType.DEF:
                    result = holder.Armor;
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            return result;
        }

        public HabilityBuilder AddCripple(ModAttributeType attr, ushort value)
        {
            _actions.Add(target =>
            {
                var crippled = GetAttributeFrom(target.Attributes, attr);
                crippled.OffsetBy(-value);
            });
            return this;
        }

        public HabilityBuilder AddBoost(ModAttributeType attr, ushort value)
        {
            _actions.Add(target =>
            {
                var boosted = GetAttributeFrom(target.Attributes, attr);
                boosted.OffsetBy(value);
            });
            return this;
        }

        public TargettableHability Build()
        {
            return new()
            {
                Name = _name,
                Description = _description,
                Caster = _caster,
                Cost = _cost,
                Actions = _actions,
            };
        }
    }

}
