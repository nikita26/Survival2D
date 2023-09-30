using Godot;

namespace Survival2D.Abstractions
{
    internal interface ICharacterPlayer : ICharacter
    {
        void Shot(Vector2 direction);
    }
}
