namespace GreedFlameTale.Model.Character
{
    /// <summary>
    /// A foul soul sealed in a giant clay statue.<br/>
    /// The Golem is very slow, but has very powerful attacks and resistance.
    /// </summary>
    class Golem : CharacterBase
    {
        public Golem(string name) : base(name)
        {
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(20),
                Stamina = new Measure(2),
                MagicPower = new Measure(0),
                AttackPower = new Measure(5),
                Armor = new Measure(0),
                NormalCost = new Measure(1),
                SpecialCost = new Measure(2),
                HealPoints = new Measure(1),
                RestPoints = new Measure(1)
            };
        }

        /// <summary>
        /// The golem is falling apart because it cannot contain the foul soul
        /// </summary>
        public override void Turn() => this.Attributes.HitPoints.DecreaseBy(new Measure(1));

        /// <summary>
        /// A powerful attack that smashes the target.
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(CharacterBase target)
        {
            var damage = this.Attributes.AttackPower.Clone();
            damage.DecreaseBy(target.Attributes.Armor);
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        /// <summary>
        /// The foul soul heals the golem
        /// and unleashes a powerful attack that ignores armor.
        /// </summary>
        /// <param name="target"></param>
        public override void SpecialAttack(CharacterBase target)
        {
            this.Attributes.HitPoints.Fill();
            var damage = this.Attributes.AttackPower;
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
