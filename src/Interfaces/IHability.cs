using System.Collections.Generic;

namespace GameInterfaces
{
    /// <summary>
    /// This interface represents a generic hability with one target.
    /// </summary>
    public interface IHability
    {
        /// <summary> The characters affected by the hability </summary>
        List<IGameCharacter> Targets {get; set;}
        
        /// <summary> The characters who can cast this hability. </summary>
        IGameCharacter Caster {get;}

        /// <summary> Check if the hability can be cast </summary>
        /// <returns> true if the all the conditions to cast the hability are cleared. </returns>
        public bool CanBeCast();

        /// <summary> Perform the hability </summary>
        /// <returns> true if the hability was successful, else false </returns>
        public bool apply();
    }

}