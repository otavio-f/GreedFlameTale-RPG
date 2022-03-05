namespace GreedFlameTale.Model.Character
{
    class Knight : GameCharacterBase
    {
        public Knight(string name)
        {
            this.Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                Magic = new Measure(0),
                AttackPower = new Measure(3),
                Armor = new Measure(1),
                NormalCost = new Measure(1),
                SpecialCost = new Measure(2),
                HealPoints = new Measure(2),
                RestPoints = new Measure(2)
            };
        }

        public override void Attack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        public override void SpecialAttack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower.Clone();
            damage.Fill();
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
