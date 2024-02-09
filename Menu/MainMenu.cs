using Godot;

namespace Survival2D.Menu
{
	public partial class MainMenu : Control
	{
		public override void _Ready()
		{
		}

		public override void _Process(double delta)
		{
		}

		private void _on_button_start_pressed()
		{
			GetTree().ChangeSceneToFile("res://Levels/Level_1/Level_1.tscn");
			GD.Print("_on_button_start_pressed");
		}

		private void _on_button_multiplayer_pressed()
		{
			GD.Print("_on_button_multiplayer_pressed");
		}

		private void _on_button_options_pressed()
		{
			GD.Print("_on_button_options_pressed");
		}

		private void _on_button_quit_pressed()
		{
			GD.Print("_on_button_quit_pressed");
			GetTree().Quit();
		}
	}
}