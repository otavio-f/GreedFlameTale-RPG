namespace GreedFlameTale.Model
{
    public record AttributeHolder
    {
        public Measure HitPoints { get; init; }
        public Measure Stamina { get; init; }
        public Measure Magic { get; init; }
        public Measure AttackPower { get; init; }
        public Measure Armor { get; init; }
        public Measure NormalCost { get; init; }
        public Measure SpecialCost { get; init; }
        public Measure HealPoints { get; init; }
        public Measure RestPoints { get; init; }
    }
}