using GreedFlameTale.Interface;

namespace GreedFlameTale.Model.Character
{
    class Knight : IGameCharacter
    {
        public string Name { get; set; }

        public AttributeHolder Attributes { get; init; }

        public Knight(string name)
        {
            Name = name;
            this.Attributes = new AttributeHolder()
            {
                HitPoints = new Measure(10),
                Stamina = new Measure(5),
                Magic = new Measure(1),
                AttackPower = new Measure(3),
                Armor = new Measure(1),
                NormalCost = new Measure(1),
                SpecialCost = new Measure(2),
                HealPoints = new Measure(3),
                RestPoints = new Measure(1)
            };
        }

        public void Attack(IGameCharacter target)
        {
            this.Attributes.Stamina.DecreaseBy(this.Attributes.NormalCost);
            var damage = this.Attributes.AttackPower - target.Attributes.Armor;
            target.Attributes.HitPoints.DecreaseBy(damage);
            target.GotAttacked(this);
        }

        public void SpecialAttack(IGameCharacter target)
        {
            this.Attributes.Stamina.DecreaseBy(this.Attributes.SpecialCost);
            var damage = this.Attributes.AttackPower + (new Measure(1));
            target.Attributes.HitPoints.DecreaseBy(damage);
            target.GotAttacked(this);
        }
    }
}
