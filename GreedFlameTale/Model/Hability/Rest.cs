using GreedFlameTale.Interface;
using static GreedFlameTale.Enum.AttributeType;

namespace GreedFlameTale.Model.Hability
{
    public class Rest : IHability
    {
        private IGameCharacter _caster;

        private string _name;

        public string Name => this._name;

        public Rest(string name, IGameCharacter caster)
        {
            this._caster = caster;
            this._name = name;
        }

        public bool CanCast() => true;

        public void Cast(params IGameCharacter[] targets)
        {
            var stamina = this._caster.GetAttribute(STAMINA);
            var restCap = this._caster.GetAttribute(REST_CAPACITY);
            stamina.IncreaseBy(restCap);
        }
    }
}