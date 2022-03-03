using GreedFlameTale.Interface;
using static GreedFlameTale.Enum.AttributeType;

namespace GreedFlameTale.Model.Hability
{
    public class Heal : IHability
    {
        private IGameCharacter _caster;

        private string _name;

        public string Name => this._name;

        public Heal(string name, IGameCharacter caster)
        {
            this._caster = caster;
            this._name = name;
        }


        public bool CanCast() => true;

        public void Cast(params IGameCharacter[] targets)
        {
            var hp = this._caster.GetAttribute(HIT_POINTS);
            var healCap = this._caster.GetAttribute(HEAL_CAPACITY);
            hp.IncreaseBy(healCap);
        }
    }
}