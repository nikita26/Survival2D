using Game1.Characters;
using Godot;

public partial class MainCharacter : Godot.CharacterBody2D, ICharacter
{
	private Vector2 _moveVector;
	private float _moveSpeed;

	public MainCharacter()
	{
		_HP = 100;
	}

	private int _HP;
	public int HP { get => _HP; }

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
			if(node is ICharacter)
			{
				var character = (ICharacter)node;
				character.GiveDamage(10);
			}
			var type = node.GetClass();
			var script = node.GetScript();
			GD.Print(node.Name);
			GD.Print(node.NativeInstance);
			GD.Print(type);
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
		base._Input(@event);
	}


	public void GiveDamage(int damage)
	{
		_HP -= damage;
	}
}
