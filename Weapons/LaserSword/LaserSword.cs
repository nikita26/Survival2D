using Godot;
using Survival2D.Abstractions;

namespace Survival2D.Weapons.LaserSword
{
	public partial class LaserSword : Area2D , IProjectile
	{
		private readonly PackedScene _resource;
		public LaserSword()
		{
			Speed = 10;
			SceneFilePath = "res://Weapons/LaserSword/LaserSword.tscn";
			_resource = ResourceLoader.Load<PackedScene>(SceneFilePath);
			BodyEntered += LaserSword_BodyEntered;
		}

		public float Damage { get; set; }

		public float Speed { get; set; }

		public Vector2 MoveVector { get; set; }

		public void Shot(Vector2 startPosition, Vector2 vectorAttack, Node spawn)
		{
			var instance = _resource.Instantiate();
			var node = instance as LaserSword;
			node.Damage = Damage;
			node.Position = startPosition;
			node.MoveVector = vectorAttack; 
			spawn.AddChild(node);
		}

		public override void _Ready()
		{
			var timer = new Timer { Autostart = true,WaitTime = 10 };
			timer.Timeout += () => { QueueFree(); };
			AddChild(timer);
			base._Ready();
		}
		public override void _Process(double delta)
		{
			var fDelta = (float)delta;
			Position += MoveVector * (float)fDelta * Speed;
			Rotate(20 * (float)delta);
			base._Process(delta);

			
		}

		private void LaserSword_BodyEntered(Node2D body)
		{
			if(body is not ICharacterPlayer && body is ICharacter character)
			{
				character.GiveDamage(Damage);
				GD.Print(body.Name);
				GD.Print("HP: "+character.HP);
				Damage/=2;
				if(Damage < 1)
					QueueFree();
			}
		}
	}
}
