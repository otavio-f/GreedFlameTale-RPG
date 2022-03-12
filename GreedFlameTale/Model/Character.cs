using GreedFlameTale.Model.Attribute;
using GreedFlameTale.Model.Hability;

namespace GreedFlameTale.Model
{
    /// <summary>
    /// Base character model
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Character's name
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The character's class. Eg.: Knight, Fighter, Mage, etc...
        /// </summary>
        public string Class { get; init; }

        /// <summary>
        /// The character attributes
        /// </summary>
        public AttributeHolder Attributes { get; init; }

        /// <summary>
        /// The set of unique habilities
        /// </summary>
        public HabilityHolder Habilities { get; init; }

        /// <summary>
        /// Constructor with a name
        /// </summary>
        /// <param name="name"></param>
        private protected Character(string name)
        {
            this.Name = name;
        }



    }
}