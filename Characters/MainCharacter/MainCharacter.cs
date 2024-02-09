using Godot;
using Survival2D.Abstractions;
using Survival2D.Weapons.LaserSword;
using Timer = Godot.Timer;

namespace Survival2D.Characters.MainCharacter
{
	public partial class MainCharacter : CharacterBody2D, ICharacterPlayer
	{
		private Vector2 _moveVector;
		private float _moveSpeed;
		private float _HP;

		private RichTextLabel _damageTextLabel;
		private ProgressBar _HPBar;
		public MainCharacter()
		{
			_HP = 100;
			Weapon = new LaserSwordWeapon { Damage = 100 };
		}

		public float HP
		{
			get => _HP;
			private set
			{
				_HP = value;
				_HPBar.Value = value;
			}
		}

        public IWeapon Weapon { get; }

        public override void _Ready()
		{
			_moveSpeed = (float)GetMeta("MoveSpeed");
			_HPBar = GetNode<ProgressBar>("HPBar");
		}

		public override void _PhysicsProcess(double delta)
		{
			MoveAndSlide();
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
					if (key.Keycode == Key.Space)
					{
						Shot(new Vector2(1, 0)); ;
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
			if (@event is InputEventScreenTouch touch)
			{
				var centerWindow = GetTree().Root.Size / 2;
				var pointAttack = touch.Position - centerWindow;
				Shot(pointAttack);
			}
			base._Input(@event);
		}


		public void GiveDamage(float damage)
		{
			if (HP > 0)
			{
				ShowDamagelabel(damage);
				HP -= damage;
				if (HP <= 0)
				{
					Death();
				}
			}
		}

		public void Death()
		{
			QueueFree();

			var level = GetTree().Root.GetChild<ILevel>(0);
			level.StopGame();
		}

		public void Shot(Vector2 direction)
		{
			Weapon.Attack(Position, direction, GetTree().Root);
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
	}
}
