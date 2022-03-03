namespace GreedFlameTale.Interface
{
    public interface IHability
    {
        string Name {get;}
        bool CanCast();
        void Cast(params IGameCharacter[] targets);
    }
}