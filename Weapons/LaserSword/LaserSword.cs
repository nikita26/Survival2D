using Godot;
using Survival2D.Abstractions;
using System;
using System.Threading.Tasks;

namespace Survival2D.Weapons.LaserSword
{
	public partial class LaserSword : Area2D , IProjectile
	{
		public LaserSword()
		{
			Speed = 10;
			SceneFilePath = "res://Weapons/LaserSword/LaserSword.tscn";
			BodyEntered += LaserSword_BodyEntered;
		}

		public float Damage { get; set; }

		public float Speed { get; set; }

		public Vector2 MoveVector { get; set; }

		public void Shot(Vector2 startPosition, Vector2 vectorAttack, Node spawn)
		{
			var resource = ResourceLoader.Load<PackedScene>(SceneFilePath);
			var instance = resource.Instantiate();
			var node = instance as LaserSword;
			node.Damage = Damage;
			node.Position = startPosition;
			node.MoveVector = vectorAttack; 
			spawn.AddChild(node);
		}

		public override void _Ready()
		{

			Task.Run(async() => {
				await Task.Delay(10000);
				QueueFree();
			});
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
				QueueFree();
			}
		}
	}
}
