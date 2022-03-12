using System;
using System.Collections.Generic;

namespace GreedFlameTale.Model.Hability
{
    public class TargettableHability
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Attribute.CharacterAttribute Cost { get; init; }
        public Character Caster { get; init; }
        public List<Action<Character>> Actions { get; init; }

        internal TargettableHability() { }

        public bool CanCast
        {
            get
            {
                var sta = this.Caster.Attributes.Stamina;
                return sta.CompareTo(this.Cost) >= 0;
            }
        }

        public void Apply(Character target) => Actions.ForEach(act => act(target));

    }

}
