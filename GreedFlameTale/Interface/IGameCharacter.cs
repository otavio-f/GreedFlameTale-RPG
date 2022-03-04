using GreedFlameTale.Model;

namespace GreedFlameTale.Interface
{
    public interface IGameCharacter
    {
        string Name { get; set; }
        AttributeHolder Attributes { get; init; }

        bool IsAlive => !this.Attributes.HitPoints.IsEmpty;
        bool CanAttack => this.Attributes.Stamina.CompareTo(this.Attributes.NormalCost) >= 0;
        bool CanSpecialAttack => this.Attributes.Stamina.CompareTo(this.Attributes.SpecialCost) >= 0;

        void GotAttacked(IGameCharacter attacker) { }
        void Attack(IGameCharacter target);
        void SpecialAttack(IGameCharacter target);
        void Rest() => this.Attributes.Stamina.IncreaseBy(this.Attributes.RestPoints);
        void Heal() => this.Attributes.Stamina.IncreaseBy(this.Attributes.HealPoints);
    }
}