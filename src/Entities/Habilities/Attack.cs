using System;
using System.Collections.Generic;
using GameInterfaces;
using Greed_Flame_Tale_RPG;
using static GameEnums.AttributeType;

namespace GameHabilities
{
    /// <summary>
    /// This class represents a basic attack, against one or multiple targets
    /// Can ignore target armor
    /// </summary>
    public class Attack : IHability
    {

        /// <inheritdoc/>
        public List<IGameCharacter> Targets { get; set; }

        /// <summary> The actual caster character. </summary>
        private IGameCharacter _caster { get; set; }

        /// <summary> The characters who can cast this hability. </summary>
        public IGameCharacter Caster { get {return this._caster; } }
        
        /// <summary> Indicates if the attack will ignore armor attribute. </summary>
        private bool _ignoreArmor { get; set; }

        /// <summary> The cost to launch the attack. </summary>
        private IClampedUnit _cost { get; set; }

        /// <summary> The cost to launch the attack. </summary>
        public IClampedUnit Cost { get {return this._cost;} }

        /// <summary> Creates a instance from a builder instance. </summary>
        /// <param name="builder"> An <see cref="AttackBuilder"/> instance </param>
        public Attack (AttackBuilder builder)
        {
            this._caster = builder.Caster;
            this.Targets = builder.Targets;
            this._ignoreArmor = builder.IgnoreArmor;
            this._cost = builder.Cost;
        }

        /// <inheritdoc/>
        public bool CanBeCast() {
            IClampedUnit sta = this.Caster.GetAttribute(STAMINA);
            IClampedUnit healCost = this.Caster.GetAttribute(BASIC_STAMINA_COST);
            return (sta.CompareTo(healCost) >= 0);
        }
        
        /// <inheritdoc/>
        public bool apply() {
            IClampedUnit cost = this._caster.GetAttribute(BASIC_STAMINA_COST);
            IClampedUnit sta = this._caster.GetAttribute(STAMINA);
            sta.DecreaseValueBy(cost);

            MeasurementUnit phy = this._caster.GetAttribute(ATTACK_POINTS) as MeasurementUnit;
            MeasurementUnit mag = this._caster.GetAttribute(MAGIC_POINTS) as MeasurementUnit;
            MeasurementUnit damage = phy + mag;
            MeasurementUnit armor;
            
            this.Targets.ForEach( target => {
                    IClampedUnit hp = target.GetAttribute(HEALTH_POINTS);
                    if (this._ignoreArmor) 
                        armor = new MeasurementUnit(0);
                    else 
                        armor = target.GetAttribute(ARMOR_POINTS) as MeasurementUnit;                    
                    hp.DecreaseValueBy(damage - armor);
                    target.AttackedBy(this._caster);
                }
            );
            return true;
        }
    }

    /// <summary>
    /// Class to build an <see cref="Attack"/>
    /// </summary>
    public class AttackBuilder
    {
        /// <summary> The targets for this attack. 
        /// Defaults to an empty <see langword="List"/>.
        /// </summary>
        internal List<IGameCharacter> Targets { get; set; } = new List<IGameCharacter>();

        /// <summary> The characters who can cast this hability. </summary>
        internal IGameCharacter Caster { get; set; }
        
        /// <summary> 
        /// Indicates if the attack will ignore armor attribute.
        /// Defaults to <see langword="false"/>.
        /// </summary>
        internal bool IgnoreArmor { get; set; } = false;

        /// <summary>
        /// The cost to launch the attack.
        /// Defaults to a zero-ed <see cref="MeasurementUnit"/>
        /// </summary>
        internal IClampedUnit Cost { get; set; } = new MeasurementUnit(0);

        /// <summary> Creates a new builder </summary>
        public static AttackBuilder attack() => new AttackBuilder();

        /// <summary> Specify the attacker. </summary>
        public AttackBuilder withAttacker(IGameCharacter caster)
        {
            this.Caster = caster;
            return this;
        }

        /// <summary> Set a single target. </summary>
        /// <param name="target"> The target </param>
        public AttackBuilder withTarget(IGameCharacter target)
        {
            this.Targets = new List<IGameCharacter>(){target};
            return this;
        }
        
        /// <summary> Set multiple targets. </summary>
        /// <param name="targets"> The target collection. </param>
        public AttackBuilder withTargets(IEnumerable<IGameCharacter> targets)
        {
            this.Targets = new List<IGameCharacter>(targets);
            return this;
        }

        /// <summary> Set cost. </summary>
        /// <param name="cost"> The cost unit. </param>
        public AttackBuilder withCost(IClampedUnit cost)
        {
            this.Cost = cost;
            return this;
        }

        // <summary> Set the attack to ignore armor. </summary>
        public AttackBuilder ignoreArmor()
        {
            this.IgnoreArmor = true;
            return this;
        }
        
        /// <summary> Set the attack to not ignore armor. </summary>
        public AttackBuilder doNotIgnoreArmor()
        {
            this.IgnoreArmor = false;
            return this;
        }

        /// <summary> Create an <see cref="Attack"/>. </summary>
        /// <returns> The <see cref="Attack"/>
        /// created from the parameters in this <see cref="AttackBuilder"/>. </returns>
        /// <exception cref="ArgumentNullException"> If the <see cref="AttackBuilder.Caster"/>
        /// was not set or is <see langword="null"/>. </exception>
        public Attack build() 
        {
            if (this.Caster == null)
                throw new ArgumentNullException("You must set the attacker");
            return new Attack(this);
        }

    }

}