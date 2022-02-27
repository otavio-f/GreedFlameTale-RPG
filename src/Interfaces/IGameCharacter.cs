using System.Collections.Generic;
using GameEnums;

namespace GameInterfaces
{
    /// <summary>
    /// This abstract class represents a character in the RPG Game
    /// with unique attributes and habilities.
    /// </summary>
    public interface IGameCharacter
    {
        /// <summary>Name of the character.</summary>
        string Name { get; set; }

        /// <summary> Attributes of the character.</summary>
        // Minimum: Health Points, Magic Points, Attack Points, Armor Points,
        // Heal Points, Rest Points
        // TODO: Track minimum attributes.
        Dictionary<AttributeType, IClampedUnit> Attributes { get; set; }

        /// <summary> Habilities of the character.</summary>
        Dictionary<string, IHability> Habilities { get; set; }

        /// <summary>Indicates if the character is alive.</summary>
        bool IsAlive { get; }

        /// <summary>Utility method to get an hability.</summary>
        public IClampedUnit GetHAbility(string hab);

        /// <summary>Utility method to get an attribute.</summary>
        public IClampedUnit GetAttribute(AttributeType attr);

        /// <summary>Action executed every turn.</summary>
        public void Turn();

        /// <summary> Action executed when attacked. </summary>
        public void AttackedBy(IGameCharacter attacker);

    }
}
