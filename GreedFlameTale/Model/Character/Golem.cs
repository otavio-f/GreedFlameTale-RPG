namespace GreedFlameTale.Model.Character
{
    class Golem : GameCharacterBase
    {
        public Golem(string name)
        {
            this.Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(20),
                Stamina = new Measure(10),
                Magic = new Measure(0),
                AttackPower = new Measure(5),
                Armor = new Measure(0),
                NormalCost = new Measure(5),
                SpecialCost = new Measure(10),
                HealPoints = new Measure(1),
                RestPoints = new Measure(1)
            };
        }

        public override void Turn()
        {
            var heal = this.Attributes.HealPoints;
            var hp = this.Attributes.HitPoints;
            hp.IncreaseBy(heal);
        }

        public override void Attack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.Attack(target);
        }

        public override void SpecialAttack(GameCharacterBase target)
        {
            var damage = this.Attributes.AttackPower + this.Attributes.AttackPower - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            base.SpecialAttack(target);
        }
    }
}
