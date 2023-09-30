namespace Survival2D.Abstractions
{
    public interface ISpawn
    {
        ICharacter Unit { get; }
        void Spawn();
    }
}
