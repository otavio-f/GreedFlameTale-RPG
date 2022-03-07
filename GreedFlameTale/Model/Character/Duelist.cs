using System;
namespace GreedFlameTale.Model.Character
{
    /// <summary>
    /// Represents a Duelist character.
    /// A nimble fighter with a lighter sword, if compared to the <see cref="Knight"/>
    /// </summary>
    class Duelist : CharacterBase
    {
        private Random _rand;
        public Duelist(string name): base(name)
        {
            this._rand = new Random();
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                MagicPower = new Measure(0),
                AttackPower = new Measure(2),
                Armor = new Measure(1),
                NormalCost = new Measure(2),
                SpecialCost = new Measure(4),
                HealPoints = new Measure(2),
                RestPoints = new Measure(2)
            };
        }

        /// <summary>
        /// Calculates the damage with critical chance
        /// </summary>
        /// <param name="minimumDmg">The resulting minimum damage</param>
        /// <param name="critChance">The chance of damage being doubled (a critical).</param>
        /// <returns>The minimum damage or doubled damage if critical.</returns>
        private Measure GetDamageOrCrit(Measure minimumDmg, int critChance)
        {
            Measure result;
            if (this._rand.Next(100) < critChance)
                result = minimumDmg + minimumDmg;
            else
                result = minimumDmg.Clone();
            result.Fill();
            return result;
        }

        /// <summary>
        /// A attack with 25% chance of striking a critical point on the target.
        /// </summary>
        /// <param name="target">The attacked character.</param>
        public override void Attack(CharacterBase target)
        {
            var damage = GetDamageOrCrit(this.Attributes.AttackPower, 25);
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        /// <summary>
        /// A attack with 50% chance of striking a critical point on the target.
        /// </summary>
        /// <param name="target"></param>
        public override void SpecialAttack(CharacterBase target)
        {
            var damage = GetDamageOrCrit(this.Attributes.AttackPower, 50);
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
