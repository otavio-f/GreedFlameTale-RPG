namespace GreedFlameTale.Model.Character
{
    /// <summary>
    /// A fast fighter that specializes on crippling and damaging a foe from the shadows.
    /// </summary>
    class Assassin : GameCharacterBase
    {
        public Assassin(string name)
        {
            this.Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(10),
                MagicPower = new Measure(1),
                AttackPower = new Measure(2),
                Armor = new Measure(0),
                NormalCost = new Measure(2),
                SpecialCost = new Measure(4),
                HealPoints = new Measure(1),
                RestPoints = new Measure(1)
            };
        }

        /// <summary>
        /// An attack that combines both magical and physical power.
        /// </summary>
        /// <param name="target">The target</param>
        public override void Attack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower + this.Attributes.MagicPower;
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        /// <summary>
        /// An attack that ignores any defense and cripples the target
        /// </summary>
        /// <param name="target"></param>
        public override void SpecialAttack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower;
            target.Attributes.HitPoints.DecreaseBy(damage);
            var cripple = this.Attributes.MagicPower;
            target.Attributes.Stamina.DecreaseBy(cripple);
            base.SpecialAttack(target);
        }
    }
}
