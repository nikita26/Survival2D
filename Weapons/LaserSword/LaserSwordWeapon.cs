using Godot;
using Survival2D.Abstractions;
using Survival2D.Weapons.LaserSword;
using System.Threading.Tasks;

namespace Survival2D.Weapons.LaserSword
{
	public partial class LaserSwordWeapon : IWeapon
	{
		private bool isAttackDelay = false;
        
		private float _damage;

        public LaserSwordWeapon()
		{
			Projectile = new LaserSword();
			AttackDelay = 500;
		}

		public string Name => "Sith laser sword";

		public float Damage
		{
			get => _damage;
			set
			{
				_damage = value;
				Projectile.Damage = value;
			}
		}

        public int AttackDelay { get; set; }

		public IProjectile Projectile { get; }

		public void Attack(Vector2 startPosition, Vector2 vectorAttack, Node spawn)
		{
			if (!isAttackDelay)
			{
				Projectile.Shot(startPosition, vectorAttack, spawn);

				isAttackDelay = true;
				Task.Factory.StartNew(async () => {
					await Task.Delay(AttackDelay);
					isAttackDelay = false;
				});
			}
		}
	}
}
