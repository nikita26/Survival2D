using Godot;
using Survival2D.Abstractions;
using System.Threading.Tasks;

public partial class EnemySpawn : Node2D, ISpawn
{
	public EnemySpawn()
	{
		Unit = new Enemy();
	}
	public ICharacter Unit { get; }

	public override void _Ready()
	{
		Task.Run(async () => {
			while (true)
			{
				await Task.Delay(3000);
				Dispatcher.SynchronizationContext.Send(_ => { Spawn(); }, null);
			}
		});

	}

	public void Spawn()
	{
		var node = Unit as Node2D; 
		var resource = ResourceLoader.Load<PackedScene>(node.SceneFilePath);
		var instance = resource.Instantiate() as Enemy;
		instance.Position = Position;
		GetTree().Root.AddChild(instance);
	}
}
