using System;
using GreedFlameTale.Interface;

namespace GreedFlameTale.Model.Hability
{
    public class Nothing : IHability
    {
        public string Name => null;

        public bool CanCast() => true;

        public void Cast(params IGameCharacter[] targets) { }
    }
}