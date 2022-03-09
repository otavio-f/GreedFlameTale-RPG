using System;

namespace GreedFlameTale.Model
{
    /// <summary>
    /// Base character model
    /// </summary>
    public class CharacterBase
    {
        /// <summary>
        /// Character's name
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The character attributes
        /// </summary>
        public AttributeHolder Attributes { get; init; }

        /// <summary>
        /// Constructor with a name
        /// </summary>
        /// <param name="name"></param>
        private protected CharacterBase(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Applies the stamina cost of attacking
        /// </summary>
        public void ApplyCost()
        {
            var sta = this.Attributes.Stamina;
            var cost = this.Attributes.NormalCost;
            sta.DecreaseBy(cost);
        }

        /// <summary>
        /// Applies the stamina cost of casting a special attack
        /// </summary>
        public void ApplySpecialCost()
        {
            var sta = this.Attributes.Stamina;
            var cost = this.Attributes.SpecialCost;
            sta.DecreaseBy(cost);
        }

        /// <summary>
        /// Is executed on every character turn
        /// </summary>
        public virtual void Turn() { }
        
        /// <summary>
        /// Action to be executed after the character is attacked
        /// </summary>
        /// <param name="attacker"></param>
        public virtual void GotAttacked(CharacterBase attacker) { }
        
        /// <summary>
        /// Attack a target.<br/>
        /// The base action only applies the cost and trigger the target reaction
        /// </summary>
        /// <param name="target">The target</param>
        public virtual void Attack(CharacterBase target) 
        {
            this.ApplyCost();
            target.GotAttacked(this);
        }

        /// <summary>
        /// Apply damage to this character's <see cref="AttributeHolder.HitPoints"/><br/>
        /// This attack triggers a reaction. See <see cref="CharacterBase.GotAttacked(CharacterBase)"/>
        /// </summary>
        /// <param name="damage">The damage</param>
        /// <param name="ignoreArmor">If <see langword="true"/>, bypass <see cref="AttributeHolder.Armor"/></param>
        /// <param name="attacker">The attacker</param>
        public void ApplyDamage(Measure damage, bool ignoreArmor, CharacterBase attacker)
        {
            if (!ignoreArmor)
                damage.DecreaseBy(this.Attributes.Armor);
            this.Attributes.HitPoints.DecreaseBy(damage);
            this.GotAttacked(attacker);
        }

        /// <summary>
        /// Cast a Special Attack against a target.<br/>
        /// This base action only applies the special cost and trigger the target reaction
        /// </summary>
        /// <param name="target">The target</param>
        public virtual void SpecialAttack(CharacterBase target)
        {
            this.ApplySpecialCost();
            target.GotAttacked(this);
        }

        /// <summary>
        /// Base action for recovering <see cref="AttributeHolder.Stamina"/>
        /// </summary>
        public virtual void Rest()
        {
            this.Attributes.Stamina.IncreaseBy(this.Attributes.RestPoints);
            this.Attributes.HitPoints.IncreaseBy(this.Attributes.HealPoints);
        }

    }
}