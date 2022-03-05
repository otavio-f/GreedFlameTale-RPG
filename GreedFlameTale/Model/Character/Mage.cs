namespace GreedFlameTale.Model.Character
{
    class Mage : GameCharacterBase
    {
        public Mage(string name)
        {
            this.Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                Magic = new Measure(3),
                AttackPower = new Measure(0),
                Armor = new Measure(0),
                NormalCost = new Measure(2),
                SpecialCost = new Measure(3),
                HealPoints = new Measure(1),
                RestPoints = new Measure(1)
            };
        }

        public override void Attack(GameCharacterBase target)
        {
            this.Attributes.Stamina.DecreaseBy(this.Attributes.NormalCost);
            var damage = this.Attributes.Magic - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            target.GotAttacked(this);
        }

        public override void SpecialAttack(GameCharacterBase target)
        {
            this.Attributes.Stamina.DecreaseBy(this.Attributes.SpecialCost);
            var damage = this.Attributes.Magic + this.Attributes.Magic - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            target.GotAttacked(this);
        }
    }
}
