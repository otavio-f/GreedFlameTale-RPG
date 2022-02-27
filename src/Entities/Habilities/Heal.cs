using System.Collections.Generic;
using GameInterfaces;
using static GameEnums.AttributeType;
using Greed_Flame_Tale_RPG;

namespace GameHabilities
{
    /// <summary>
    /// This class represents a heal in multiple characters
    /// </summary>
    public class Heal : IHability
    {

        /// <inheritdoc/>
        public List<IGameCharacter> Targets { get; set; }

        /// <summary> The actual caster character. </summary>
        private IGameCharacter _caster { get; set; }

        /// <summary> The characters who can cast this hability. </summary>
        public IGameCharacter Caster { get {return this._caster; } }

        /// <summary> Creates a instance of self-healing. </summary>
        /// <param name="caster">The healer and target </param>
        public Heal (IGameCharacter caster)
        {
            this._caster = caster;
            this.Targets = new List<IGameCharacter>{caster};
        }

        /// <summary> Creates a instance of mass-healing. </summary>
        /// <param name="caster">The healer and target </param>
        /// <param name="targets">The healing targets </param>
        public Heal (IGameCharacter caster, List<IGameCharacter> targets)
        {
            this._caster = caster;
            this.Targets = new List<IGameCharacter> (targets);
        }

        /// <inheritdoc/>
        public bool CanBeCast() {
            IClampedUnit sta = this.Caster.GetAttribute(STAMINA);
            IClampedUnit healCost = this.Caster.GetAttribute(BASIC_STAMINA_COST);
            return (sta.CompareTo(healCost) >= 0);
        }
        
        /// <inheritdoc/>
        public bool apply() {
            this.Targets.ForEach( target => {
                    IClampedUnit hp = target.GetAttribute(HEALTH_POINTS);
                    IClampedUnit heal = target.GetAttribute(HEAL_CAPACITY);
                    hp.IncreaseValueBy(heal);
                }
            );
            return true;
        }
    }

}