namespace Game1.Characters
{
    internal interface ICharacter
    {
        int HP { get; }

        void GiveDamage(int damage);
    }
}
