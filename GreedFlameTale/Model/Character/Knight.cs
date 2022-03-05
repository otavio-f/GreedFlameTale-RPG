namespace GreedFlameTale.Model.Character
{
    class Knight : GameCharacterBase
    {
        /// <summary>
        /// A powerful warrior with high resilience and resistance
        /// </summary>
        /// <param name="name"></param>
        public Knight(string name)
        {
            this.Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                MagicPower = new Measure(0),
                AttackPower = new Measure(3),
                Armor = new Measure(1),
                NormalCost = new Measure(2),
                SpecialCost = new Measure(3),
                HealPoints = new Measure(2),
                RestPoints = new Measure(1)
            };
        }

        /// <summary>
        /// The warrior slashes the foe.
        /// </summary>
        /// <param name="target">The foe</param>
        public override void Attack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower.Clone();
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        /// <summary>
        /// He shashes the foe with all his strength, ignoring any armor.
        /// </summary>
        /// <param name="target"></param>
        public override void SpecialAttack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower.Clone();
            damage.Fill();
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
