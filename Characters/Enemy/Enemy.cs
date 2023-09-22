using Game1.Characters;
using Godot;
using System;

public partial class Enemy : CharacterBody2D , ICharacter
{
	public Enemy()
	{
		_HP = 100;
	}
	private int _HP;
	public int HP { get => _HP; }

	public void GiveDamage(int damage)
	{
		_HP -= damage;
		if(_HP <= 0)
		{
			QueueFree();
		}
	}

	public override void _Ready()
	{
		Velocity = new Vector2(0, 100);

		base._Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
}
