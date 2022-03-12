namespace GreedFlameTale.Model.Hability
{
    /// <summary>
    /// A set of basic habilities for a <see cref="Character"/>
    /// </summary>
    public class HabilityHolder
    {
        /// <summary>
        /// An attack hability, used agains a foe
        /// </summary>
        public TargettableHability Attack { get; init; }

        /// <summary>
        /// A custom attack, the most powerful attack for any <see cref="Character"/>
        /// </summary>
        public TargettableHability SpecialAttack { get; init; }

        /// <summary>
        /// The action to be executed after a character suffers an attack.
        /// </summary>
        public TargettableHability AttackReaction { get; init; } = new();

        /// <summary>
        /// THe action to be executed at every turn of a character.
        /// </summary>
        public TargettableHability AtTurn { get; init; } = new();

        /// <summary>
        /// A set of actions to be executed when resting.
        /// </summary>
        public TargettableHability Rest { get; init; }
    }
}
