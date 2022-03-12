namespace GreedFlameTale.Model.Attribute
{
    /// <summary>
    /// A builder for a <see cref="AttributeHolder"/>.</br>
    /// Default values for unassigned attributes are <see langword="0"/>;
    /// </summary>
    public class AttributeHolderBuilder
    {
        private ushort _hp = 0;
        private ushort _sta = 0;
        private ushort _mp = 0;
        private ushort _atk = 0;
        private ushort _def = 0;
        private ushort _heal = 0;
        private ushort _starec = 0;

        /// <summary>
        /// Construct a new builder
        /// </summary>
        public AttributeHolderBuilder() { }

        /// <summary>
        /// Configure hit points of the attributes
        /// </summary>
        /// <param name="value">The Hit Points value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithHp(ushort value)
        {
            this._hp = value;
            return this;
        }

        /// <summary>
        /// Configure stamina points of the attributes
        /// </summary>
        /// <param name="value">The stamina value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithSta(ushort value)
        {
            this._sta = value;
            return this;
        }

        /// <summary>
        /// Configure magic points of the attributes
        /// </summary>
        /// <param name="value">The Magic Points value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithMp(ushort value)
        {
            this._mp = value;
            return this;
        }

        /// <summary>
        /// Configure attack points of the attributes
        /// </summary>
        /// <param name="value">The Attack Points value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithAtk(ushort value)
        {
            this._atk = value;
            return this;
        }

        /// <summary>
        /// Configure armor points of the attributes
        /// </summary>
        /// <param name="value">The Armor Points value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithDef(ushort value)
        {
            this._def = value;
            return this;
        }

        /// <summary>
        /// Configure heal power of the attributes
        /// </summary>
        /// <param name="value">The Heal Power value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithHeal(ushort value)
        {
            this._heal = value;
            return this;
        }

        /// <summary>
        /// Configure stamina recovery of the attributes
        /// </summary>
        /// <param name="value">The Stamina Recovery Power value</param>
        /// <returns></returns>
        public AttributeHolderBuilder WithRecovery(ushort value)
        {
            this._starec = value;
            return this;
        }

        /// <summary>
        /// Build an <see cref="AttributeHolder"/> from this builder instance.
        /// </summary>
        /// <returns>A new <see cref="AttributeHolder"/></returns>
        public AttributeHolder Build()
        {
            return new()
            {
                HitPoints = new(_hp),
                Stamina = new(_sta),
                AttackPower = new(_atk),
                MagicPower = new(_mp),
                Armor = new(_def),
                HealPoints = new(_heal),
                RestPoints = new(_starec)
            };
        }
    }
}