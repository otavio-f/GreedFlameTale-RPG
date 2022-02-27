using System.Collections.Generic;
using GameInterfaces;
using static GameEnums.AttributeType;
using Greed_Flame_Tale_RPG;

namespace GameHabilities
{
    /// <summary>
    /// This class represents a rest action of a single character
    /// </summary>
    public class Rest : IHability
    {

        /// <inheritdoc/>
        public List<IGameCharacter> Targets { get; set; }

        /// <summary> The actual caster character. </summary>
        private IGameCharacter _caster { get; set; }

        /// <summary> The characters who can cast this hability. </summary>
        public IGameCharacter Caster { get {return this._caster; } }

        /// <summary> Creates a instance of rest. </summary>
        /// <param name="caster">The caster and target </param>
        public Rest (IGameCharacter caster)
        {
            this._caster = caster;
            this.Targets = new List<IGameCharacter>{caster};
        }

        /// <inheritdoc/>
        public bool CanBeCast() { return true; }
        
        /// <inheritdoc/>
        public bool apply() {
            this.Targets.ForEach( target => {
                    IClampedUnit sta = target.GetAttribute(STAMINA);
                    IClampedUnit rest = target.GetAttribute(REST_CAPACITY);
                    rest.IncreaseValueBy(rest);
                }
            );
            return true;
        }
    }

}