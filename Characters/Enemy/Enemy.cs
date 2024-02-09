using Godot;
using Survival2D.Abstractions;
using Timer = Godot.Timer;

namespace Survival2D.Characters.Enemy
{
	public partial class Enemy : CharacterBody2D, ICharacter
	{

		private RichTextLabel _damageTextLabel;
		private AnimatedSprite2D _animation;
		private Sprite2D _sprite;
		private CharacterBody2D _targetAttack;

		private float _HP;

		public Enemy()
		{
			_HP = 100;
			SceneFilePath = "res://Characters/Enemy/Enemy.tscn";

		}

		public float HP { get => _HP; }

		public IWeapon Weapon { get; }

		public void GiveDamage(float damage)
		{
			if (_HP > 0)
			{
				ShowDamagelabel(damage);
				_HP -= damage;
				if (_HP <= 0)
				{
					Death();
				}
			}
		}

		public void Death()
		{
			Velocity = Vector2.Zero;
			_animation.Visible = true;
			_sprite.Visible = false;

			_animation.Play("death");

			var timer = new Timer() { Autostart = true, WaitTime = 0.5 };
			timer.Timeout += () => { QueueFree(); };
			AddChild(timer);
		}

		public override void _Ready()
		{
			_animation = GetNode<AnimatedSprite2D>("YodaAnimation");
			_sprite = GetNode<Sprite2D>("Sprite2D");

			_targetAttack = (CharacterBody2D)GetNode<ICharacterPlayer>("../MainCharacter");

			base._Ready();
		}
		public override void _PhysicsProcess(double delta)
		{
			MoveAndSlide();
			for (int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				var node = ((Node)collision.GetCollider());
				if (node is ICharacterPlayer character)
				{
					character.GiveDamage(1);
				}
			}
		}
		public override void _Process(double delta)
		{
			MoveToPlayer();
		}

		private void ShowDamagelabel(float damage)
		{

			if (_damageTextLabel is null)
			{
				_damageTextLabel = new RichTextLabel
				{
					Scale = new Vector2(7, 7),
					Size = new Vector2(100, 50),
					Position = new Vector2(-70, -330),
					ZIndex = 1,
					Visible = false
				};
				AddChild(_damageTextLabel);
			}

			_damageTextLabel.Text = $"-{(int)damage}";
			_damageTextLabel.Visible = true;

			var timer = new Timer() { Autostart = true, WaitTime = 0.3 };
			timer.Timeout += () => { _damageTextLabel.Visible = false; timer.QueueFree(); };
			AddChild(timer);
		}

		private void MoveToPlayer()
		{
			if (_targetAttack is not null && HP > 0)
				Velocity = _targetAttack.Position - Position;
		}
	}
}
