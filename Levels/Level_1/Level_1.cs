using Godot;
using Survival2D.Abstractions;
using Survival2D.Tools;
using Survival2D.Tools.Score;
using System;

namespace Survival2D.Levels
{
	public partial class Level_1 : Node2D, ILevel
	{
		public override void _Ready()
		{
			var scoreProvider = new ScoreFileProvider();
			var score = new ScoreItem()
			{
				PlayerName = System.Environment.UserName,
				Score = 100,
				Date = DateTime.Now,
			};

			scoreProvider.WriteScore(score);
		}
		public void StopGame()
		{
			GetTree().ChangeSceneToFile("res://Menu/MainMenu.tscn");
			QueueFree();
		}
	}
}
