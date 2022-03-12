namespace GreedFlameTale.Model.Attribute
{
    /// <summary>
    /// This record is The attributes collection.
    /// </summary>
    public class AttributeHolder
    {
        /// <summary>
        /// Hit Points, aka HP
        /// </summary>
        public CharacterAttribute HitPoints { get; init; }
        /// <summary>
        /// Stamina, how much habilities can be cast
        /// </summary>
        public CharacterAttribute Stamina { get; init; }
        /// <summary>
        /// Magic Power, aka MP
        /// </summary>
        public CharacterAttribute MagicPower { get; init; }
        /// <summary>
        /// AttackPower, aka Strength
        /// </summary>
        public CharacterAttribute AttackPower { get; init; }
        /// <summary>
        /// Armor Points, aka Defense
        /// </summary>
        public CharacterAttribute Armor { get; init; }
        /// <summary>
        /// Heal Points, how much <see cref="HitPoints"/> are recovered on healing
        /// </summary>
        public CharacterAttribute HealPoints { get; init; }
        /// <summary>
        /// How much <see cref="Stamina"/> is recovered on resting
        /// </summary>
        public CharacterAttribute RestPoints { get; init; }

        public bool IsDead
        {
            get => this.HitPoints.IsEmpty;
        }

        internal AttributeHolder() { }
    }
}