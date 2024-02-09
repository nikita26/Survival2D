using Godot;
using Survival2D.Abstractions;
using Survival2D.Characters.Enemy;
using System.Threading.Tasks;

namespace Survival2D.Spawns.EnemySpawn
{
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
					await Task.Delay(2000);
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
			var level = GetTree().Root.GetChild<ILevel>(0);
			((Node)level).AddChild(instance);
		}
	}
}
