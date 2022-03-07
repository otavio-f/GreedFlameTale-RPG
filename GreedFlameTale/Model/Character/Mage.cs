namespace GreedFlameTale.Model.Character
{
    class Mage : CharacterBase
    {
        /// <summary>
        /// A powerful mage capable of powerful spells
        /// </summary>
        /// <param name="name"></param>
        public Mage(string name) : base(name)
        {
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                MagicPower = new Measure(3),
                AttackPower = new Measure(0),
                Armor = new Measure(0),
                NormalCost = new Measure(2),
                SpecialCost = new Measure(3),
                HealPoints = new Measure(3),
                RestPoints = new Measure(3)
            };
        }

        /// <summary>
        /// Tme mage unleashes a magic projectile at the target
        /// </summary>
        /// <param name="target">The target</param>
        public override void Attack(CharacterBase target)
        {
            var damage = this.Attributes.MagicPower.Clone();
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        /// <summary>
        /// The mage increases it's own <see cref="AttributeHolder.MagicPower"/> and unleases a magic explosion at the target.
        /// </summary>
        /// <param name="target">The target</param>
        public override void SpecialAttack(CharacterBase target)
        {
            this.Attributes.MagicPower.OffsetBy(+1);
            this.Attributes.Stamina.OffsetBy(+1);
            var damage = this.Attributes.MagicPower;
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
