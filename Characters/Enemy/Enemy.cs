using Godot;
using Survival2D.Abstractions;
using System.Threading.Tasks;

public partial class Enemy : CharacterBody2D, ICharacter
{

	private RichTextLabel _damageTextLabel;
	private AnimatedSprite2D _animation;
	private Sprite2D _sprite;
	public Enemy()
	{
		_HP = 100;
		SceneFilePath = "res://Characters/Enemy/Enemy.tscn";

	}

	private float _HP;
	public float HP { get => _HP; }

	public IWeapon Weapon { get; }

	public void GiveDamage(float damage)
	{
		if(_HP > 0)
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

		Velocity = new Vector2(0, 100);
		_animation = GetNode<AnimatedSprite2D>("YodaAnimation");
		_sprite = GetNode<Sprite2D>("Sprite2D");
		base._Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
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

		Task.Run(async () =>
		{
			await Task.Delay(300);
			Dispatcher.SynchronizationContext.Send(_ => {
				_damageTextLabel.Visible = false;
			}, null);
		});
	}
}
