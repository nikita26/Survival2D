namespace Survival2D.Abstractions
{
    public interface ICharacter
    {
        float HP { get; }

        IWeapon Weapon { get; }

        void GiveDamage(float damage);

        void Death();
    }
}
