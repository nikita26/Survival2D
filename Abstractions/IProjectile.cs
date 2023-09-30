using Godot;

namespace Survival2D.Abstractions
{
    public interface IProjectile
    {
        float Damage { get; set; }
        
        float Speed { get; set; }

        Vector2 MoveVector{ get; set; }

        void Shot(Vector2 startPosition, Vector2 vectorAttack, Node spawn);

        string SceneFilePath { get; }
    }
}
