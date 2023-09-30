using Godot;

namespace Survival2D.Abstractions
{
    public interface IWeapon
    {
        string Name { get; }
        float Damage { get; set; }
        int AttackDelay { get; set; }
        IProjectile Projectile { get; }

        void Attack(Vector2 startPosition, Vector2 vectorAttack, Node spawn);
    }
}
