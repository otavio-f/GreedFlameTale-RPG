using System;

namespace GreedFlameTale.Model
{
    /// <summary>
    /// Base character model
    /// </summary>
    public abstract class GameCharacterBase
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
        /// If the character is alive
        /// </summary>
        public bool IsAlive => !this.Attributes.HitPoints.IsEmpty;
        
        /// <summary>
        /// If the character can attack
        /// </summary>
        public bool CanAttack => this.Attributes.Stamina.CompareTo(this.Attributes.NormalCost) >= 0;
        
        /// <summary>
        /// If the character can cast a special attack
        /// </summary>
        public bool CanSpecialAttack => this.Attributes.Stamina.CompareTo(this.Attributes.SpecialCost) >= 0;

        /// <summary>
        /// Applies the stamina cost of attacking
        /// </summary>
        private void ApplyCost()
        {
            var sta = this.Attributes.Stamina;
            var cost = this.Attributes.NormalCost;
            sta.DecreaseBy(cost);
        }

        /// <summary>
        /// Applies the stamina cost of casting a special attack
        /// </summary>
        private void ApplySpecialCost()
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
        public virtual void GotAttacked(GameCharacterBase attacker) { }
        
        /// <summary>
        /// Attack a target.<br/>
        /// The base action only applies the cost and trigger the target reaction
        /// </summary>
        /// <param name="target">The target</param>
        public virtual void Attack(GameCharacterBase target)
        {
            this.ApplyCost();
            target.GotAttacked(this);
        }

        /// <summary>
        /// Cast a Special Attack against a target.<br/>
        /// This base action only applies the special cost and trigger the target reaction
        /// </summary>
        /// <param name="target">The target</param>
        public virtual void SpecialAttack(GameCharacterBase target)
        {
            this.ApplySpecialCost();
            target.GotAttacked(this);
        }

        /// <summary>
        /// Base action for recovering <see cref="AttributeHolder.Stamina"/>
        /// </summary>
        public virtual void Rest() => this.Attributes.Stamina.IncreaseBy(this.Attributes.RestPoints);

        /// <summary>
        /// Base action for recovering <see cref="AttributeHolder.HitPoints"/>
        /// </summary>
        public virtual void Heal() => this.Attributes.HitPoints.IncreaseBy(this.Attributes.HealPoints);
    }
}