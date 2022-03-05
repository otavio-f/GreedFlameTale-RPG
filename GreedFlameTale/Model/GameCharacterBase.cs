namespace GreedFlameTale.Model
{
    public abstract class GameCharacterBase
    {
        public string Name { get; init; }
        public AttributeHolder Attributes { get; init; }

        public bool IsAlive => !this.Attributes.HitPoints.IsEmpty;
        public bool CanAttack => this.Attributes.Stamina.CompareTo(this.Attributes.NormalCost) >= 0;
        public bool CanSpecialAttack => this.Attributes.Stamina.CompareTo(this.Attributes.SpecialCost) >= 0;

        private void ApplyCost()
        {
            var sta = this.Attributes.Stamina;
            var cost = this.Attributes.NormalCost;
            sta.DecreaseBy(cost);
        }

        private void ApplySpecialCost()
        {
            var sta = this.Attributes.Stamina;
            var cost = this.Attributes.SpecialCost;
            sta.DecreaseBy(cost);
        }

        public virtual void Turn() { }
        public virtual void GotAttacked(GameCharacterBase attacker) { }
        public virtual void Attack(GameCharacterBase target)
        {
            this.ApplyCost();
            target.GotAttacked(this);
        }

        public virtual void SpecialAttack(GameCharacterBase target)
        {
            this.ApplySpecialCost();
            target.GotAttacked(this);
        }

        public virtual void Rest() => this.Attributes.Stamina.IncreaseBy(this.Attributes.RestPoints);
        public virtual void Heal() => this.Attributes.Stamina.IncreaseBy(this.Attributes.HealPoints);
    }
}