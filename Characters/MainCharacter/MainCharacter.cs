using Godot;
using Survival2D.Abstractions;

public partial class MainCharacter : CharacterBody2D, ICharacterPlayer
{
	private Vector2 _moveVector;

	private float _moveSpeed;

	public MainCharacter()
	{
		_HP = 100;
		Weapon = new LaserSwordWeapon { Damage = 30 };
	}

	private float _HP;
	public float HP { get => _HP; }

	public override void _Ready()
	{

		_moveSpeed = (float)GetMeta("MoveSpeed");
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
		
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			var node = ((Node)collision.GetCollider());
			if(node is ICharacter character)
			{
				character.GiveDamage(10);
			}
		}
		//var collision = MoveAndCollide(_moveVector);
		//if(collision != null)
		//{
		//	GD.Print(collision.GetClass());
		//	GD.Print(collision.GetNormal().ToString());
		//	GD.Print(collision.GetInstanceId());
		//	GD.Print("");
		//	var position = collision.GetPosition();
		//}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey key)
		{
			if (key.IsPressed())
			{
				if (key.Keycode == Key.W)
				{
					//_moveVector.Y = -_moveSpeed;
					Velocity = new Vector2(Velocity.X, -_moveSpeed);
				}
				if (key.Keycode == Key.S)
				{
					//_moveVector.Y = _moveSpeed;
					Velocity = new Vector2(Velocity.X, _moveSpeed);

				}
				if (key.Keycode == Key.A)
				{
					//_moveVector.X = -_moveSpeed;
					Velocity = new Vector2(-_moveSpeed, Velocity.Y);

				}
				if (key.Keycode == Key.D)
				{
					//_moveVector.X = _moveSpeed;
					Velocity = new Vector2(_moveSpeed, Velocity.Y);
				}
				if(key.Keycode == Key.Space)
				{
					Shot(new Vector2(1,0));;
				}
			}
			else
			{
				if (key.Keycode == Key.W)
				{
					//_moveVector.Y = 0;
					Velocity = new Vector2(Velocity.X, 0);
				}
				if (key.Keycode == Key.S)
				{
					//_moveVector.Y = 0;
					Velocity = new Vector2(Velocity.X, 0);
				}
				if (key.Keycode == Key.A)
				{
					//_moveVector.X = 0;
					Velocity = new Vector2(0, Velocity.Y);
				}
				if (key.Keycode == Key.D)
				{
					//_moveVector.X = 0;
					Velocity = new Vector2(0, Velocity.Y);
				}
			}
		}
		if(@event is InputEventScreenTouch touch)
		{
			var centerWindow = GetTree().Root.Size / 2;
			var pointAttack = touch.Position - centerWindow;
			Shot(pointAttack);
		}
		base._Input(@event);
	}

	public IWeapon Weapon { get; }

	public void GiveDamage(float damage)
	{
		_HP -= damage;
	}

	public void Death()
	{

	}

	public void Shot(Vector2 direction)
	{
		Weapon.Attack(Position, direction, GetTree().Root);
	}
}
